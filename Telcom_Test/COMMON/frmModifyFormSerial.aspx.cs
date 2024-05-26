using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class COMMON_frmModifyFormSerial : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objservicerHndlr = new clsServiceHandler();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
        LoadNullData();
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

    private void LoadNullData()
    {
        sdsAvailable.SelectCommand = "";
        sdsAvailable.DataBind();       
        SqlDataSource1.SelectCommand = "";
        SqlDataSource1.DataBind();       
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        string strSql = "";
        //############## check serial and add to grid #####################
        string strGetSerialNo = objservicerHndlr.GetFromSerial(txtserailNo.Text);
        string[] strGetResult = strGetSerialNo.Split('*');
        string strSerialNo = strGetResult[0];
        string strStatus = strGetResult[1];
        string strCustomrMbl = strGetResult[2];
        string strAgentPhNo = strGetResult[3];
        if (!strSerialNo.Equals(""))
        {
            if (strStatus != "A")
            {
                strSql = " SELECT CL.CLINT_NAME,AL.ACCNT_NO,ASD.STATUS,ASD.CUSTOMER_MOBILE_NO,ASD.AGENT_MOBILE_NO,ASD.SERIAL_NO FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL WHERE ASD.CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN AND AL.CLINT_ID=CL.CLINT_ID AND SERIAL_NO='" + txtserailNo.Text + "'";
                SqlDataSource1.SelectCommand = strSql;
                SqlDataSource1.DataBind();
                gdvSearch.DataBind();
                if (gdvSearch.Rows.Count > 0)
                {
                    sdsAvailable.SelectCommand = "";
                    sdsAvailable.DataBind();
                    gdvAvailable.DataBind();
                    gdvSearch.Visible = true;
                    div1.Visible = true;
                    BtnUpdate.Visible = false;
                    txtbAvailabl.Text = "";
                    lblAvlbl.Text = "";
                }
            }
            else
            {
                lblMsg.Text = "This serial number is unused.";
                div1.Visible = false;
                gdvSearch.Visible = false;
            }
        }
        else
        {
            lblMsg.Text = "This serial number is out of range.";
            div1.Visible = false;
            gdvSearch.Visible = false;
        }
    }   

    private void ShowOldserialInfo(string strOldSerialNo)
    {
        string strSql = "";
        strSql = " SELECT CL.CLINT_NAME,AL.ACCNT_NO,ASD.STATUS,ASD.CUSTOMER_MOBILE_NO,ASD.AGENT_MOBILE_NO,ASD.SERIAL_NO FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL WHERE AL.ACCNT_MSISDN(+)=ASD.CUSTOMER_MOBILE_NO AND AL.CLINT_ID=CL.CLINT_ID(+) AND SERIAL_NO='" + strOldSerialNo + "'";
        try
        {
            SqlDataSource1.SelectCommand = strSql;
            SqlDataSource1.DataBind();
            gdvSearch.DataBind();
            if (gdvSearch.Rows.Count > 0)
            {
                gdvSearch.Visible = true;
            }

        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void SelectFrmserialUpdate(string strSrialNo)
    {
        string strSql = "";
        strSql = " SELECT CL.CLINT_NAME,AL.ACCNT_NO,ASD.STATUS,ASD.CUSTOMER_MOBILE_NO,ASD.AGENT_MOBILE_NO,ASD.SERIAL_NO "
               + " FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL WHERE ASD.CUSTOMER_MOBILE_NO=AL.ACCNT_MSISDN "
               + " AND AL.CLINT_ID=CL.CLINT_ID AND SERIAL_NO='" + strSrialNo + "'";
        try
        {
            sdsAvailable.SelectCommand = strSql;
            sdsAvailable.DataBind();
            gdvAvailable.DataBind();
            if (gdvAvailable.Rows.Count > 0)
            {
                gdvAvailable.Visible = true;
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void BtnSearchAvlb_Click(object sender, EventArgs e)
    {
        string strSql = "";
        string strGetSerialNo = objservicerHndlr.GetFromSerial(txtbAvailabl.Text);
        string[] strGetResult = strGetSerialNo.Split('*');
        string strSerialNo = strGetResult[0];
        string strStatus = strGetResult[1];
        string strCustomrMbl = strGetResult[2];
        string strAgentPhNo = strGetResult[3];
        if (!strSerialNo.Equals(""))
        {
            if (strStatus == "A")
            {
                if (strCustomrMbl == "")
                {
                    if (strAgentPhNo == "")
                    {
                        //########### search for available data #####################                      
                        strSql = "SELECT CL.CLINT_NAME,AL.ACCNT_NO,ASD.STATUS,ASD.CUSTOMER_MOBILE_NO,ASD.AGENT_MOBILE_NO,ASD.SERIAL_NO FROM ACCOUNT_SERIAL_DETAIL ASD,ACCOUNT_LIST AL,CLIENT_LIST CL WHERE AL.ACCNT_MSISDN(+)=ASD.CUSTOMER_MOBILE_NO AND AL.CLINT_ID=CL.CLINT_ID(+) AND SERIAL_NO='" + txtbAvailabl.Text + "'";
                        sdsAvailable.SelectCommand = strSql;
                        sdsAvailable.DataBind();
                        gdvAvailable.DataBind();
                        gdvAvailable.Visible = true;
                        if (gdvAvailable.Rows.Count > 0)
                        {
                            BtnUpdate.Visible = true;
                            lblAvlbl.Text = " This serial number is available.";
                        }
                        else
                        {
                            BtnUpdate.Visible = false;
                        }
                    }
                }
            }
            else
            {
                lblAvlbl.Text = "This serial is already used.";
                BtnUpdate.Visible = false;
                strSql = "";
                sdsAvailable.SelectCommand = strSql;
                sdsAvailable.DataBind();
                gdvAvailable.DataBind();
                gdvAvailable.Visible = false;
            }
        }
        else
        {
            lblAvlbl.Text = "This serial is out of range.";
            BtnUpdate.Visible = false;
        }
    }
    protected void BtnUpdate_Click(object sender, EventArgs e)
    {
        lblAvlbl.Text = "";       
        //########### update data ################
        Label lblNewFormSerial = (Label)gdvAvailable.Rows[0].Cells[1].FindControl("lblNewSerialNumber");
        Label lbloldSerilNumber = (Label)gdvSearch.Rows[0].Cells[1].FindControl("lblOldSerialNumber");
        Label lblCustomerMobileNo = (Label)gdvSearch.Rows[0].Cells[4].FindControl("lblCustMob");
        if (gdvAvailable.Rows.Count>0 && gdvSearch.Rows.Count>0)
        {
            //########## get old serial info ###########################
            string strGetSerialNo = objservicerHndlr.GetFromSerial(lbloldSerilNumber.Text.ToString()); 
            string[] strGetResult = strGetSerialNo.Split('*');
            string strOldSerialNo = strGetResult[0]; 
            string strStatus = strGetResult[1];
            string strCustMobNo = strGetResult[2];  
            string strAgentPhnNo = strGetResult[3];
            DateTime  strDate = Convert.ToDateTime(strGetResult[4].ToString());
            string strRequestID = strGetResult[5];
            string strComDisbrs = strGetResult[6];
            string strActiveDate =String.Format("{0:dd-MMM-yyyy HH:mm:ss}", strDate);
            string strUpdate = objservicerHndlr.UpdateFrmSerialBoth(strCustMobNo, strOldSerialNo, strStatus,
                                                             strAgentPhnNo, lblNewFormSerial.Text.ToString(),
                                                             strActiveDate, strRequestID, strComDisbrs);   
            if (strUpdate == "")
            {
                SelectFrmserialUpdate(lblNewFormSerial.Text.ToString());
                ShowOldserialInfo(lbloldSerilNumber.Text.ToString()); 
                lblMsg.Text = "Update Successfully.";
                BtnUpdate.Visible = false;
                SaveAuditInfo("Modify", "Old SL No=" + txtserailNo.Text.Trim() + " Change to New SL No=" + txtbAvailabl.Text.Trim() + " Customer Mobile No=" + lblCustomerMobileNo.Text.Trim());
            }
        }
        else
        {
            lblMsg.Text = "Please check serial number.";
            BtnUpdate.Visible = false;
        }
    }
    private void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    //override protected void OnInit(EventArgs e)
    //{
    //    BtnUpdate.Attributes.Add("onclick", "javascript:" +
    //              BtnUpdate.ClientID + ".disabled=true;" +
    //              this.GetPostBackEventReference(BtnUpdate));
    //    InitializeComponent();
    //    base.OnInit(e);
    //}

    //private void InitializeComponent()
    //{
    //    this.BtnUpdate.Click +=
    //            new System.EventHandler(this.BtnUpdate_Click);
    //    this.Load += new System.EventHandler(this.Page_Load);
    //}
}
