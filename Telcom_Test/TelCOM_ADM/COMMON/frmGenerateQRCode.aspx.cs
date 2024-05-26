using QRCoder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class COMMON_frmGenerateQRCode : System.Web.UI.Page
{
    private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();

                PopulateAccountRank();
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        // Start - Check active session
        try
        {
            string sess_id = HttpContext.Current.Session.SessionID;
            string strSessID = objSysAdmin.GetActiveSess(sess_id, Session["UserID"].ToString());

            if (strSessID == Session["Sess_ID"].ToString())
            {
                Session.Timeout = Convert.ToInt32(Session["SessionOut"].ToString());
            }
            else
            {
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
        // End - Check active session
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        string strFileName = "";
        string strAccountRankId = ddlAccountRank.SelectedItem.Value;

        string strSql = "SELECT AOL.CLINT_BANK_ACC_NO, CL.CLINT_NAME, CL.CLINT_ADDRESS1, AOL.ENCRYPT_DATA, AOL.IS_PRINT FROM ACCOUNT_LIST AL, ACCOUNT_POS_LIST AOL, CLIENT_LIST CL WHERE AL.ACCNT_ID = AOL.ACCNT_ID AND AL.CLINT_ID = CL.CLINT_ID AND AOL.ENCRYPT_DATA IS NOT NULL AND AL.ACCNT_RANK_ID = '" + strAccountRankId + "' AND SUBSTR(AOL.CLINT_BANK_ACC_NO,12,1) = '1' AND AOL.IS_PRINT = 'A' AND AL.ACCNT_NO = '017865431361'";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

        if (oDs.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow item in oDs.Tables[0].Rows)
            {
                string strEncryptCode = item["ENCRYPT_DATA"].ToString();
                string strSubWalletNumber = item["CLINT_BANK_ACC_NO"].ToString();
                string strClientName = item["CLINT_NAME"].ToString();
                strFileName = strClientName + " (" + strSubWalletNumber + ")";

                var url = string.Format("https://chart.googleapis.com/chart?cht=qr&chs={1}x{2}&chl={0}", strEncryptCode, 500, 500);
                WebResponse response = default(WebResponse);
                Stream remoteStream = default(Stream);
                StreamReader readStream = default(StreamReader);
                WebRequest request = WebRequest.Create(url);
                response = request.GetResponse();
                remoteStream = response.GetResponseStream();
                readStream = new StreamReader(remoteStream);
                System.Drawing.Image img = System.Drawing.Image.FromStream(remoteStream);
                strFileName = strFileName.Replace("/", "");
                strFileName = strFileName.Replace(":", "");
                strFileName = strFileName.Replace("\\", "");
                strFileName = strFileName.Replace("*", "");
                strFileName = strFileName.Replace("\"", "");
                strFileName = strFileName.Replace(">", "");
                strFileName = strFileName.Replace("|", "");
                img.Save(AppDomain.CurrentDomain.BaseDirectory + "QRFiles\\" + strFileName + ".png");
                response.Close();
                remoteStream.Close();
                readStream.Close();
                img.Dispose();

                strFileName = strFileName + ".png";
            }
            LoadPrintReport(strFileName, strAccountRankId);
        }
        else
        {
            lblMsg.Text = "QR Code has not been generated till now.";
            return;
        }
    }

    private void LoadPrintReport(string strFileName, string strAccountRankId)
    {
        string strClientSig = "";
        try
        {
            string strSql = "SELECT AOL.CLINT_BANK_ACC_NO, CL.CLINT_NAME, CLINT_ADDRESS1, AOL.ENCRYPT_DATA, AR.RANK_TITEL, AOL.QR_IMAGE, AOL.IS_PRINT FROM ACCOUNT_LIST AL, ACCOUNT_POS_LIST AOL, CLIENT_LIST CL, ACCOUNT_RANK AR WHERE AL.ACCNT_ID = AOL.ACCNT_ID AND AL.CLINT_ID = CL.CLINT_ID AND AL.ACCNT_RANK_ID = AR.ACCNT_RANK_ID AND AOL.ENCRYPT_DATA IS NOT NULL AND AL.ACCNT_RANK_ID = '" + strAccountRankId + "' AND SUBSTR(CLINT_BANK_ACC_NO,12,1) = '1' AND AOL.IS_PRINT = 'A' AND AL.ACCNT_NO = '017865431361'";

            DataSet oDS1 = new DataSet();
            oDS1 = objServiceHandler.ExecuteQuery(strSql);

            for (int index = 0; index < oDS1.Tables[0].Rows.Count; index++)
            {
                string strImageName = oDS1.Tables[0].Rows[index]["CLINT_NAME"] +" ("+ oDS1.Tables[0].Rows[index]["CLINT_BANK_ACC_NO"] + ").png";
                //strImageName = oDS1.Tables[0].Rows[index]["CLINT_BANK_ACC_NO"] + ".png";
                strImageName = strImageName.Replace("/", "");
                strImageName = strImageName.Replace(":", "");
                strImageName = strImageName.Replace("\\", "");
                strImageName = strImageName.Replace("*", "");
                strImageName = strImageName.Replace("\"", "");
                strImageName = strImageName.Replace(">", "");
                strImageName = strImageName.Replace("|", "");
                strClientSig = this.Server.MapPath("~/QRFiles/" + strImageName);
                LoadImage(oDS1.Tables[0].Rows[index], "QR_IMAGE", strClientSig);  
            }

            Session["Dual"] = oDS1.Tables[0];
            Session["ReportSQL"] = "NOTSQL";
            Session["CompanyBranch"] = "";
            //Session["ReportSQL"] = strSql;
            Session["RequestForm"] = "../COMMON/frmGenerateQRCode.aspx";
            Session["ReportFile"] = "../COMMON/crpBarcode_Report.rpt";
            Response.Redirect("../COM/COM_ReportView.aspx");
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnpl1Download_Click(object sender, EventArgs e)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "QRFiles\\";
        string filename = path + lblCodeName1.Text + ".png";

        Response.ContentType = "image/png";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + lblCodeName1.Text);
        Response.TransmitFile(filename);
        Response.End();
    }
    protected void btnpl2Download_Click(object sender, EventArgs e)
    {
        string path = AppDomain.CurrentDomain.BaseDirectory + "QRFiles\\";
        string filename = path + lblCodeName1.Text + ".png";

        Response.ContentType = "image/png";
        Response.AddHeader("Content-Disposition", "attachment; filename=" + lblCodeName1.Text + ".png");
        Response.TransmitFile(filename);
        Response.End();
    }

    private void LoadImage(DataRow objDataRow, string strImageField, string FilePath)
    {
        try
        {
            FileStream fs = new FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] Image = new byte[fs.Length];
            fs.Read(Image, 0, Convert.ToInt32(fs.Length));
            fs.Close();
            objDataRow[strImageField] = Image;
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            lblMsg.Text = "NO Image found.";
        }
    }

    private void PopulateAccountRank()
    {
        string strSql = "SELECT ACCNT_RANK_ID, RANK_TITEL FROM ACCOUNT_RANK WHERE GRADE NOT IN ('0') AND STATUS = 'A'";
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);

        ddlAccountRank.DataSource = oDs;
        ddlAccountRank.DataValueField = "ACCNT_RANK_ID";
        ddlAccountRank.DataTextField = "RANK_TITEL";
        ddlAccountRank.DataBind();
    }
}