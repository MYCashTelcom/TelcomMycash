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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;

public partial class COMMON_frmDigitalKYC_Information : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private string strUserName = string.Empty;
    private string strPassword = string.Empty;
    DateTime regFDate = DateTime.Now;
    DateTime regTDate = DateTime.Now;
    DateTime lowestDateOfReg = DateTime.MinValue;
    bool isMobileOrNID = false;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {  
                DateTime dt = DateTime.Now;
                string strSQL = "";
                if (Session["DigitalKYC"] != null)
                {
                    string strDigitalKYC = Session["DigitalKYC"].ToString();
                    strSQL = "UPDATE DIGITAL_KYC_INFO SET IS_PROCESSING = 'N' WHERE DIGITAL_KYC_ID = '" + strDigitalKYC + "'";
                    DataSet ds = objServiceHandler.ExecuteQuery(strSQL);
                    // Session["DigitalKYC"] = null;
                    // Session["Category"] = null;
                    // Session["DateFrom"] = null;
                    // Session["DateTo"] = null;
                }
                strSQL = "SELECT MIN(REGISTRATION_DATE) FROM DIGITAL_KYC_INFO";
                DataSet dsForDate = objServiceHandler.ExecuteQuery(strSQL);
                dtpRegFDate.Date = dsForDate != null ? dsForDate.Tables[0].Rows.Count > 0 ? Convert.ToDateTime(dsForDate.Tables[0].Rows[0][0]) : dt : dt;                
                LoadRequestList();
                //if (dptFromDate.DateString != "")
                //{
                //    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-1));
                //    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
                //    LoadRequestList();
                //}
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch(Exception ex)
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

    public void LoadRequestList(bool isSearchFromMobileOrNID = false, string searchString = "", int page = 0)
    {
        GetDataMessage();
        //bool isVerified = false, string strSQL = "SELECT SERIAL_NO, DIGITAL_KYC_ID, DKI.CLINT_MOBILE,DKI.CLINT_NAME, DKI.CLINT_FATHER_NAME, DKI.CLINT_MOTHER_NAME, CLIENT_PRE_ADDRESS, REGISTRATION_DATE,  AGENT_ACCNT_NO, DKI.IS_UPDATE, DKI.IS_PROCESSING, CL.CLINT_NAME AS CN, CLINT_ADDRESS1, IDENTITY_TYPE, CLINT_NATIONAL_ID, REMARKS FROM DIGITAL_KYC_INFO DKI, ACCOUNT_LIST AL, CLIENT_LIST CL  WHERE DKI.AGENT_ACCNT_NO = AL.ACCNT_NO AND CL.CLINT_ID = AL.CLINT_ID AND IS_UPDATE = 'N' ORDER BY SERIAL_NO"
        if(Session["Category"] != null)
        {
            ddlSearchOption.SelectedValue = Session["Category"].ToString();
            Session["Category"] = null;
        }
        if (Session["DateFrom"] != null)
        {
            dtpRegFDate.DateString = Session["DateFrom"].ToString();
            Session["DateFrom"] = null;
        }
        if(Session["DateTo"] != null)
        {
            dtpRegTDate.DateString = Session["DateTo"].ToString();
            Session["DateTo"] = null;
        }


        string strSQL = "";
        string searchOption = "";
        bool isVerified = false;
        bool isVerifiedByShow = false;
        

        if (!isSearchFromMobileOrNID)
        {
            regFDate = dtpRegFDate.Date;
            regTDate = dtpRegTDate.Date;

            switch (ddlSearchOption.SelectedValue)
            {
                case "verified":
                    searchOption = " AND IS_REGISTER = 'Y' AND IS_UPDATE = 'Y'";
                    isVerified = true;
                    isVerifiedByShow = true;
                    break;
                case "cancel":
                    searchOption = "(+) AND IS_UPDATE = 'N' AND IS_REGISTER = 'E' AND IS_PROCESSING IN ('C','CWS') AND REMARKS IS NOT NULL";
                    break;
                case "onproc":
                    searchOption = " AND IS_PROCESSING = 'Y' AND IS_REGISTER = 'Y'";
                    isVerified = true;
                    break;
                case "all":
                    searchOption = " AND IS_REGISTER = 'Y'";
                    //searchOption = "";
                    isVerifiedByShow = true;
                    break;
                default:
                    searchOption = " AND IS_UPDATE = 'N' AND IS_REGISTER = 'Y' AND REMARKS IS NULL";
                    break;
            }
            searchString += searchOption;
        }
        else
        {
            regFDate = lowestDateOfReg;
            regTDate = DateTime.Now;
        }
        // strSQL = "SELECT SERIAL_NO, DIGITAL_KYC_ID, DKI.CLINT_MOBILE,DKI.CLINT_NAME, DKI.CLINT_FATHER_NAME, DKI.CLINT_MOTHER_NAME, CLIENT_PRE_ADDRESS, REGISTRATION_DATE,  AGENT_ACCNT_NO,DKI.IS_UPDATE, DKI.IS_PROCESSING, CL.CLINT_NAME AS CN, CLINT_ADDRESS1, IDENTITY_TYPE, CLINT_NATIONAL_ID, REMARKS, (SELECT C.CLINT_NAME FROM CLIENT_LIST C INNER JOIN ACCOUNT_LIST ALV ON ALV.CLINT_ID = C.CLINT_ID AND C.VERIFIED_BY = ALV.ACCNT_ID WHERE C.CLINT_ID = CL.CLINT_ID) VERIFIED_BY FROM DIGITAL_KYC_INFO DKI, ACCOUNT_LIST AL, CLIENT_LIST CL  WHERE DKI.AGENT_ACCNT_NO = AL.ACCNT_NO AND CL.CLINT_ID = AL.CLINT_ID " + searchString + " AND TRUNC(REGISTRATION_DATE) BETWEEN TRUNC(TO_DATE('" + regFDate + "', 'MM/DD/YYYY HH:MI:SS AM')) AND TRUNC(TO_DATE('" + regTDate + "', 'MM/DD/YYYY HH:MI:SS AM')) ORDER BY SERIAL_NO";

        strSQL = "SELECT SERIAL_NO,CASE WHEN DKI.CLIENT_GENDER='M' THEN 'MALE' WHEN DKI.CLIENT_GENDER='F' THEN 'FEMALE' ELSE 'OTHERS' END AS CLINT_GENDER,CASE WHEN DKI.LOCATION_TYPE='U' THEN 'URBAN' WHEN DKI.LOCATION_TYPE='R' THEN 'RURAL' ELSE 'NULL' END AS CUSTOMER_AREA, DIGITAL_KYC_ID, DKI.CLINT_MOBILE,DKI.CLINT_NAME, DKI.CLINT_FATHER_NAME, DKI.CLINT_MOTHER_NAME, CLIENT_PRE_ADDRESS, REGISTRATION_DATE,  AGENT_ACCNT_NO,DKI.IS_UPDATE, DKI.IS_PROCESSING, CL.CLINT_NAME AS CN, CLINT_ADDRESS1, IDENTITY_TYPE, CLINT_NATIONAL_ID, REMARKS, (SELECT CLV.CLINT_NAME FROM ACCOUNT_LIST ALV, CLIENT_LIST CLV WHERE ALV.CLINT_ID = CLV.CLINT_ID AND ALV.ACCNT_ID = CL.KYC_UPDATED_BY) VERIFIED_BY FROM DIGITAL_KYC_INFO DKI, CLIENT_LIST CL WHERE '+88' || DKI.CLINT_MOBILE = CL.CLINT_MOBILE " + searchString + " AND TRUNC(REGISTRATION_DATE) BETWEEN TRUNC(TO_DATE('" + regFDate + "', 'MM/DD/YYYY HH:MI:SS AM')) AND TRUNC(TO_DATE('" + regTDate + "', 'MM/DD/YYYY HH:MI:SS AM')) AND REQUEST_BY IS NULL ORDER BY SERIAL_NO";
		
        //if (!txtRequestParty.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND SOURCE_ACCNT_NO LIKE '%" + txtRequestParty.Text + "%'";
        //}
        //if (!txtSubscriberNo.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND SUBSCRIBER_MOBILE_NO LIKE '%" + txtSubscriberNo.Text + "%'";
        //}
        
        //strSQL = strSQL + " ORDER BY TRAN_DATE DESC";
        
        try
        {
            sdsRequest.SelectCommand = strSQL;
            if ((Session["ItCanceled"] != null && Convert.ToBoolean(Session["ItCanceled"])) || (Session["ItVerified"] != null && Convert.ToBoolean(Session["ItVerified"])))
            {
                Session["ItCanceled"] = null;
                Session["ItVerified"] = null;
                gdvRequest.PageIndex = Session["LastPageNumber"] != null ? Convert.ToInt32(Session["LastPageNumber"]) : 0;
            }
            else
            {
                gdvRequest.PageIndex = page;
            }
            sdsRequest.DataBind();
            if (isVerified)
            {
                gdvRequest.Columns[15].Visible = false;
            }
            else
            {
                gdvRequest.Columns[15].Visible = true;
            }
            if (isVerifiedByShow)
            {
                gdvRequest.Columns[19].Visible = true;
            }
            else
            {
                gdvRequest.Columns[19].Visible = false;
            }
            gdvRequest.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    protected void btnVerifiedPDF_Click(object sender, EventArgs e)
    {
        //string imageLocation = @"D:\MPAY\MT_WS_MYCASH_QR_38\KYC_Files\";//demo path
        string imageLocation = @"D:\MPAY\MT_WS_MYCASH_QR\MT_WS_MYCASH_QR_45\KYC_Files\";//Live Path
        string strSql = "";
        string strClientSig = "";
        Button btn = (Button)sender;
        GridViewRow row = (GridViewRow)btn.NamingContainer;
        string strRequestId = gdvRequest.DataKeys[row.RowIndex].Value.ToString();
        try
        {
            strSql = "SELECT   d.IMG_FILE LOGO,         d.IMG_FILE IMG_CUSTOMER,         d.IMG_FILE IMG_SINATURE,         d.IMG_FILE IMG_NID_FONT,         d.IMG_FILE IMG_NID_BACK,         d.*,         c.*,         I.IDNTIFCTION_NAME,         asd.SERIAL_NO SN,         (SELECT   DISTRICT_NAME            FROM   MANAGE_DISTRICT           WHERE   DISTRICT_ID = d.CLIENT_PRE_DIST_ID)            PREDIS,         (SELECT   THANA_NAME            FROM   MANAGE_THANA           WHERE   THANA_ID = d.CLIENT_PRE_THANA_ID)            PRETHANA,         (SELECT   DISTRICT_NAME            FROM   MANAGE_DISTRICT           WHERE   DISTRICT_ID = d.CLIENT_PER_DIST_ID)            PERDIS,         (SELECT   THANA_NAME            FROM   MANAGE_THANA           WHERE   THANA_ID = d.CLIENT_PER_THANA_ID)            PERTHANA,         R.RELATIONSHIP_TITLE,         O.OCCUPATION_TITLE,         (SELECT   CLINT_ADDRESS1            FROM      CLIENT_LIST CL                   INNER JOIN                      ACCOUNT_LIST AL                   ON CL.CLINT_ID = AL.CLINT_ID           WHERE   AL.ACCNT_NO = D.AGENT_ACCNT_NO)            AgentAddress,            '+88' || d.CLINT_MOBILE , c.CLINT_MOBILE  FROM   DIGITAL_KYC_INFO d,         CLIENT_LIST c,         ACCOUNT_SERIAL_DETAIL asd,         IDENTIFICATION_SETUP I,         RELATIONSHIP R,         OCCUPATION O WHERE       DIGITAL_KYC_ID = '" + strRequestId + "'         AND '+88' || d.CLINT_MOBILE = c.CLINT_MOBILE         AND '+88' || d.CLINT_MOBILE = asd.CUSTOMER_MOBILE_NO         AND d.IDENTITY_TYPE = I.IDNTIFCTION_ID         AND R.RELATIONSHIP_ID = d.RELATIONSHIP_ID         AND O.OCCUPATION_ID = d.OCCUPATION_ID";

            DataSet ds = objServiceHandler.ExecuteQuery(strSql);
            //imageLocation = "http://10.11.1.9:98/KYC_Files/";
            //ImgCustomer.ImageUrl = imageLocation + rows[i]["CLINT_IMG"].ToString();
            //ImgSignature.ImageUrl = imageLocation + rows[i]["SIGNATURE_IMG"].ToString();
            //ImgNIDFront.ImageUrl = imageLocation + rows[i]["NID_FRONT_IMG"].ToString();
            //ImgNIDBack.ImageUrl = imageLocation + rows[i]["NID_BACK_IMG"].ToString();
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int index = 0; index < ds.Tables[0].Rows.Count; index++)
                {
                    if (ds.Tables[0].Rows[index]["CLINT_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["CLINT_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        //byte[] b = imageToByteArray(ImgCustomer.);
                        LoadImage(ds.Tables[0].Rows[index], "IMG_CUSTOMER", strClientSig);
                    }
                    if (ds.Tables[0].Rows[index]["SIGNATURE_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["SIGNATURE_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        LoadImage(ds.Tables[0].Rows[index], "IMG_SINATURE", strClientSig);
                    }
                    if (ds.Tables[0].Rows[index]["NID_FRONT_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["NID_FRONT_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        LoadImage(ds.Tables[0].Rows[index], "IMG_NID_FONT", strClientSig);
                    }
                    if (ds.Tables[0].Rows[index]["NID_BACK_IMG"].ToString() != "")
                    {
                        strClientSig = imageLocation + ds.Tables[0].Rows[index]["NID_BACK_IMG"].ToString();
                        // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                        LoadImage(ds.Tables[0].Rows[index], "IMG_NID_BACK", strClientSig);
                    }

                    strClientSig = this.Server.MapPath("~/Images/mycash_logo.jpg");
                    // strClientSig = this.Server.MapPath("~/Barcode/Lab_Report/" + "011241804170000004" + ".jpg");
                    LoadImage(ds.Tables[0].Rows[index], "LOGO", strClientSig);
                }

                Session["Dual"] = ds.Tables[0];
                Session["ReportSQL"] = "NOTSQL";



                SaveAuditInfo("View", " Employee Report");
                //Session["ReportSQL"] = strSql;
                Session["RequestForm"] = "../COMMON/frmDigitalKYC_ViewReport.aspx";
                Session["ReportFile"] = "../COMMON/DigitalKYC_ViewReport.rpt";
                Response.Redirect("../COM/COM_ReportView.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Wrong", "alert('Client Not Registered!');", true);
            }


        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    private void LoadImage(DataRow objDataRow, string strImageField, string FilePath)
    {
        try
        {
            //FileStream fs = new FileStream(FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //byte[] Image = new byte[fs.Length];
            //fs.Read(Image, 0, Convert.ToInt32(fs.Length));
            //fs.Close();
            //System.Drawing.Image img = System.Drawing.Image.FromFile(FilePath);
            //byte[] bArr = imageToByteArray(img);
            byte[] imageBytes = null;
            using (var webClient = new WebClient())
            {
                imageBytes = webClient.DownloadData(FilePath);
            }
            objDataRow[strImageField] = imageBytes;
        }
        catch (Exception ex)
        {
            //lblMessage2.Text = "NO Image found.";
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        txtNIDSearch.Text = String.Empty;
        txtMobileSearch.Text = String.Empty;
        txtPageSearch.Text = String.Empty;
        LoadRequestList();
    }
    protected void btnSearchMobile_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtMobileSearch.Text))
        {
            string searchString = " AND IS_REGISTER = 'Y' AND DKI.CLINT_MOBILE ='" + txtMobileSearch.Text + "'";
            //isMobileOrNID = true;
            ddlSearchOption.SelectedValue = "all";
            txtNIDSearch.Text = String.Empty;
            LoadRequestList(true, searchString);
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Please write a mobile number";
        }
    }
    protected void btnSearchNID_Click(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(txtNIDSearch.Text))
        {
            string searchString = " AND IS_REGISTER = 'Y' AND DKI.CLINT_NATIONAL_ID ='" + txtNIDSearch.Text + "'";
            //isMobileOrNID = true;
            ddlSearchOption.SelectedValue = "all";
            txtMobileSearch.Text = String.Empty;
            LoadRequestList(true, searchString);
        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Please write a NID number";
        }
    }
    protected void btnShowReport_Click(object sender, EventArgs e) // Make Successful
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string strRequestId = gdvRequest.DataKeys[row.RowIndex].Value.ToString(); //string strRequestId = row.Cells[1].Text;
            string strSQL = "";
            
            if (!String.IsNullOrEmpty(strRequestId))
            {
                strSQL = "SELECT * FROM DIGITAL_KYC_INFO WHERE DIGITAL_KYC_ID = '" + strRequestId + "' AND IS_PROCESSING = 'N'";

                DataSet ds = objServiceHandler.ExecuteQuery(strSQL);
                if (ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        strSQL = "UPDATE DIGITAL_KYC_INFO SET IS_PROCESSING = 'Y' WHERE DIGITAL_KYC_ID = '" + strRequestId + "'";

                        ds = objServiceHandler.ExecuteQuery(strSQL);
                        Session["DigitalKYC"] = strRequestId;
                        Session["Category"] = ddlSearchOption.SelectedValue;
                        Session["DateFrom"] = dtpRegFDate.DateString;
                        Session["DateTo"] = dtpRegTDate.DateString;

                        Response.Redirect("../Common/frmDigitalKYC_ViewReport.aspx",false);
                    }
                    else
                    {
                        strSQL = "SELECT SERIAL_NO, DIGITAL_KYC_ID, DKI.CLINT_MOBILE,DKI.CLINT_NAME, DKI.CLINT_FATHER_NAME, DKI.CLINT_MOTHER_NAME, CLIENT_PRE_ADDRESS, REGISTRATION_DATE,  AGENT_ACCNT_NO, CL.CLINT_NAME AS CN, CLINT_ADDRESS1, IDENTITY_TYPE, CLINT_NATIONAL_ID FROM DIGITAL_KYC_INFO DKI, ACCOUNT_LIST AL, CLIENT_LIST CL WHERE DKI.AGENT_ACCNT_NO = AL.ACCNT_NO AND CL.CLINT_ID = AL.CLINT_ID AND IS_UPDATE = 'N' AND IS_PROCESSING = 'N' ORDER BY SERIAL_NO";

                        ds = objServiceHandler.ExecuteQuery(strSQL);
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            string returnMessage = "" + ds.Tables[0].Rows[0]["SERIAL_NO"];
                            lblMsg.Visible = true;
                            lblMsg.Text = "This user already have been processing. You may try " + returnMessage;
                            ScriptManager.RegisterStartupScript(this, GetType(), "Success", "alert('" +lblMsg.Text + "');", true);
                        }
                    }
                }
                else
                {
                    
                }
            }
            
            //string strResult = objServiceHandler.MakeTopupSuccess(strRequestId);

            //if (strResult.Equals("Successful"))
            //{
            //    string strRemarks = "Manual topup reconcile successful. TXN : " + strRequestId;
            //    SaveAuditInfo("Topup Reconcile", strRemarks);
            //    LoadRequestList();
            //}
            //else
            //{
            //    lblMsg.Text = "Manual Reconcile Failed. Try Again";
            //}
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        Export();
    }
    private void Export(string strType = "msexcel")
    {
        try
        {
            string strSQL = "";
            string searchOption = "";
            bool isVerified = false;
            bool isVerifiedByShow = false;
            string searchString = "";

            if (String.IsNullOrEmpty(txtMobileSearch.Text) && String.IsNullOrEmpty(txtNIDSearch.Text))
            {
                regFDate = dtpRegFDate.Date;
                regTDate = dtpRegTDate.Date;

                switch (ddlSearchOption.SelectedValue)
                {
                    case "verified":
                        searchOption = " AND IS_UPDATE = 'Y'";
                        isVerified = true;
                        isVerifiedByShow = true;
                        break;
                    case "cancel":
                        searchOption = " AND IS_UPDATE = 'N' AND REMARKS IS NOT NULL";
                        break;
                    case "onproc":
                        searchOption = " AND IS_PROCESSING = 'Y'";
                        isVerified = true;
                        break;
                    case "all":
                        searchOption = "";
                        isVerifiedByShow = true;
                        break;
                    default:
                        searchOption = " AND IS_UPDATE = 'N'";
                        break;
                }
                searchString += searchOption;
            }
            else
            {
                regFDate = lowestDateOfReg;
                regTDate = DateTime.Now;
            }
            strSQL = "SELECT SERIAL_NO, CASE WHEN DKI.CLIENT_GENDER='M' THEN 'MALE' WHEN DKI.CLIENT_GENDER='F' THEN 'FEMALE' ELSE 'OTHERS' END AS CLINT_GENDER,CASE WHEN DKI.LOCATION_TYPE='U' THEN 'URBAN' WHEN DKI.LOCATION_TYPE='R' THEN 'RURAL' ELSE 'NULL' END AS CUSTOMER_AREA,DIGITAL_KYC_ID, DKI.CLINT_MOBILE,DKI.CLINT_NAME, DKI.CLINT_FATHER_NAME, DKI.CLINT_MOTHER_NAME, CLIENT_PRE_ADDRESS, REGISTRATION_DATE,  AGENT_ACCNT_NO,DKI.IS_UPDATE, DKI.IS_PROCESSING, CL.CLINT_NAME AS CN, CLINT_ADDRESS1, IDENTITY_TYPE, CLINT_NATIONAL_ID, REMARKS, (SELECT C.CLINT_NAME FROM CLIENT_LIST C INNER JOIN ACCOUNT_LIST ALV ON ALV.CLINT_ID = C.CLINT_ID AND C.VERIFIED_BY = ALV.ACCNT_ID WHERE C.CLINT_ID = CL.CLINT_ID) VERIFIED_BY FROM DIGITAL_KYC_INFO DKI, ACCOUNT_LIST AL, CLIENT_LIST CL  WHERE DKI.AGENT_ACCNT_NO = AL.ACCNT_NO AND CL.CLINT_ID = AL.CLINT_ID " + searchString + " AND TRUNC(REGISTRATION_DATE) BETWEEN TRUNC(TO_DATE('" + regFDate + "', 'MM/DD/YYYY HH:MI:SS AM')) AND TRUNC(TO_DATE('" + regTDate + "', 'MM/DD/YYYY HH:MI:SS AM')) ORDER BY SERIAL_NO";

            DataSet ds = objServiceHandler.ExecuteQuery(strSQL);

            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            fileName = "DigitalKYC";
            //------------------------------------------Report File xl processing   -------------------------------------


            strHTML = strHTML + @"<style>td {    mso-number-format: '\@';}</style><table border='0' width='100%'>";
            strHTML = strHTML + "<tr><td COLSPAN=8 align=center style='border-right:none' expan=true><h2 align=center> Digital KYC Information </h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";

            strHTML = strHTML + "<tr style='text-align:center;font-weight:bold;'>";
            strHTML = strHTML + "<th valign='middle'>Sl</th>";
            strHTML = strHTML + "<th valign='middle'>Customer Mobile/ Wallet ID</th>";
            strHTML = strHTML + "<th valign='middle'>Customer Name</th>";
            strHTML = strHTML + "<th valign='middle'>Registration Date And Time</th>";
            strHTML = strHTML + "<th valign='middle'>Agent Number</th>";
            strHTML = strHTML + "<th valign='middle'>Customer Area</th>";
            strHTML = strHTML + "<th valign='middle'>Gender</th>";
            strHTML = strHTML + "<th valign='middle'>National ID</th>";
            strHTML = strHTML + "<th valign='middle'>Remarks</th>";
            if (isVerifiedByShow)
            {
                strHTML = strHTML + "<th valign='middle'>Verified By</th>";
            }
            strHTML = strHTML + "</tr>";

            if (ds.Tables[0].Rows.Count > 0)
            {
                //int SerialNo = 1;
                foreach (DataRow prow in ds.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + prow["SERIAL_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["CLINT_MOBILE"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["CLINT_NAME"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["REGISTRATION_DATE"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["AGENT_ACCNT_NO"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["CUSTOMER_AREA"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["CLINT_GENDER"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["CLINT_NATIONAL_ID"].ToString() + "</td>";
                    strHTML = strHTML + " <td> " + prow["REMARKS"].ToString() + "</td>";
                    if (isVerifiedByShow)
                    {
                        strHTML = strHTML + " <td> " + prow["VERIFIED_BY"].ToString() + "</td>";
                    }

                    strHTML = strHTML + "</tr>";
                    //SerialNo = SerialNo + 1;
                }
            }

            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td COLSPAN=8 style='text-align:left;font-weight:bold;'> " + lblCountMsg.Text + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            //strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", fileName);
            clsGridExport.ExportToMSExcel(fileName, strType, strHTML, "landscape");

            lblMsg.ForeColor = Color.White;
            lblMsg.Text = "Report Generated Successfully...";
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void gdvRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvRequest.PageIndex = e.NewPageIndex;
        gdvRequest.SelectedIndex = -1;
        Session["LastPageNumber"] = e.NewPageIndex;
        LoadRequestList();
    }
    protected void btnVerify_Click(object sender, EventArgs e) // Reversed
    {
        try
        {
            Button btn = (Button)sender;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            string strRequestId = row.Cells[0].Text;
            
            //string strResult = objServiceHandler.MakeTopupReverse(strRequestId);

            //if (strResult.Equals("Successful"))
            //{
            //    string strRemarks = "Manual topup reconcile reverse. TXN : " + strRequestId;
            //    SaveAuditInfo("Topup Reconcile", strRemarks);
            //    LoadRequestList();
            //}
            //else
            //{
            //    lblMsg.Text = "Manual Reconcile Failed. Try Again";
            //}
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }   
    protected void gdvRequest_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadRequestList();
    }
    protected void gdvRequest_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LoadRequestList();
    }
    protected void gdvRequest_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        LoadRequestList();
        //--------update note -------------
        string strTopupTrnasId = gdvRequest.Rows[0].Cells[0].Text.ToString();
        string strRequest_ID = gdvRequest.Rows[0].Cells[1].Text.ToString();
        string strOldvalue = e.OldValues["SUBSCRIBER_TYPE"].ToString();
        string strNewValue = e.NewValues["SUBSCRIBER_TYPE"].ToString();
        objServiceHandler.UpdateTopupNote(strOldvalue,strNewValue,strTopupTrnasId);        
        string strUpdate = "Update: " + strOldvalue + "-to-" + strNewValue +",TopupTrans_ID:"+strTopupTrnasId+",Request_ID:"+strRequest_ID;
        SaveAuditInfo("Update",strUpdate);       
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }
    protected void gdvRequest_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button3_Click(object sender, EventArgs e) // Refresh
    {
        List<string> lstGetValue = new List<string>();

        //Get the button that raised the event
        Button btn = (Button)sender;
        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;

        for (int intCnt = 0; intCnt < gvr.Controls.Count; intCnt++)
        {
            int intCountRow = gvr.RowIndex;
            lstGetValue.Add(gdvRequest.Rows[intCountRow].Cells[intCnt].Text);
            
        }
        string strTOPUP_TRAN_ID = lstGetValue[0].ToString();

        string strOWNER_CODE = lstGetValue[17].ToString();
        string strRefresh = objServiceHandler.Getrefresh(strTOPUP_TRAN_ID, strOWNER_CODE);       
        lblMsg.Visible = true;
        lblMsg.Text = strRefresh;
        LoadRequestList();
    }
    //protected void gdvRequest_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)gdvRequest.BottomPagerRow.Cells[0].FindControl("ddlPaging");
    //    gdvRequest.PageIndex = ddl.SelectedIndex;
    //    //gdvRequest.PageIndex = e.NewSelectedIndex;
       
    //    gdvRequest.DataBind();
    //}
    protected void btnPageGo_Click(object sender, EventArgs e)
    {
        int result = 0, page = 0;
        if (Int32.TryParse(txtPageSearch.Text.Trim(),out result))
        {
            page = Convert.ToInt32(txtPageSearch.Text.Trim());
            page -= 1;
        }

        if (page < 0) page = 0;
        if (page >= gdvRequest.PageCount) page = gdvRequest.PageCount - 1;
        LoadRequestList(false, "", page);
    }
    //protected void gdvRequest_DataBound(object sender, EventArgs e)
    //{
    //    DropDownList ddl = (DropDownList)gdvRequest.BottomPagerRow.Cells[0].FindControl("ddlPaging");

    //    for (int cnt = 0; cnt < gdvRequest.PageCount; cnt++)
    //    {
    //        int curr = cnt + 1;
    //        ListItem item = new ListItem(curr.ToString());
    //        if (cnt == gdvRequest.PageIndex)
    //        {
    //            item.Selected = true;
    //        }

    //        ddl.Items.Add(item);

    //    }
    //}
    protected void ddlPaging_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Retrieve the pager row.
        GridViewRow pagerRow = gdvRequest.BottomPagerRow;

        // Retrieve the DropDownList and Label controls from the row.
        DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("PageDropDownList");
        Label pageLabel = (Label)pagerRow.Cells[0].FindControl("CurrentPageLabel");

        if (pageList != null)
        {

            // Create the values for the DropDownList control based on 
            // the  total number of pages required to display the data
            // source.
            for (int i = 0; i < gdvRequest.PageCount; i++)
            {

                // Create a ListItem object to represent a page.
                int pageNumber = i + 1;
                ListItem item = new ListItem(pageNumber.ToString());

                // If the ListItem object matches the currently selected
                // page, flag the ListItem object as being selected. Because
                // the DropDownList control is recreated each time the pager
                // row gets created, this will persist the selected item in
                // the DropDownList control.   
                if (i == gdvRequest.PageIndex)
                {
                    item.Selected = true;
                }

                // Add the ListItem object to the Items collection of the 
                // DropDownList.
                pageList.Items.Add(item);

            }

        }

        if (pageLabel != null)
        {

            // Calculate the current page number.
            int currentPage = gdvRequest.PageIndex + 1;

            // Update the Label control with the current page information.
            pageLabel.Text = "Page " + currentPage.ToString() +
              " of " + gdvRequest.PageCount.ToString();

        }    
    }
    protected void gdvRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //if (!isMobileOrNID)
            //{
                if (ddlSearchOption.SelectedValue == "verified")
                {
                    e.Row.BackColor = Color.LawnGreen;
                    gdvRequest.Style.Remove("mGrid");// = WebControl.DisabledCssClass;
                    gdvRequest.CssClass = "mGridModify";
                }
                else if (ddlSearchOption.SelectedValue == "nonverified")
                {
                    //gdvRequest.CssClass = "mGrid";
                    gdvRequest.Style.Remove("mGrid");
                    gdvRequest.CssClass = "mGridModify";
                    if (!String.IsNullOrEmpty(e.Row.Cells[16].Text.Trim()) && e.Row.Cells[16].Text != "&nbsp;")
                    {
                        e.Row.Visible = false;
                    }
                    else
                    {
                        e.Row.BackColor = Color.White;
                    }
                }
                else if (ddlSearchOption.SelectedValue == "cancel")
                {
                    gdvRequest.Style.Remove("mGrid");
                    gdvRequest.CssClass = "mGridModify";
                    e.Row.BackColor = Color.FromArgb(255, 130, 113); 
                }
                else if (ddlSearchOption.SelectedValue == "onproc")
                {
                    gdvRequest.Style.Remove("mGrid");
                    gdvRequest.CssClass = "mGridModify";
                    e.Row.BackColor = Color.Yellow;
                    e.Row.Cells[15].Enabled = false;
                    (e.Row.FindControl("btnVerifiedPDF") as Button).Visible = false;
                }
                else if (ddlSearchOption.SelectedValue == "all")
                {
                    gdvRequest.Style.Remove("mGrid");
                    gdvRequest.CssClass = "mGridModify";
                    
                    if (e.Row.Cells[17].Text == "Y")
                    {
                        e.Row.BackColor = Color.LawnGreen;
                        //e.Row.Cells[15].Enabled = false;
                        (e.Row.FindControl("btnVerifiedPDF") as Button).Visible = true;
                        (e.Row.FindControl("btnShowReport") as Button).Visible = false;
                    }
                    else if (e.Row.Cells[18].Text == "Y")
                    {
                        e.Row.BackColor = Color.Yellow;
                        e.Row.Cells[15].Enabled = false;
                        (e.Row.FindControl("btnVerifiedPDF") as Button).Visible = false;
                    }
                    else if (e.Row.Cells[17].Text == "N" && !String.IsNullOrEmpty(e.Row.Cells[16].Text.Trim()) && e.Row.Cells[16].Text != "&nbsp;")
                    {
                        e.Row.BackColor = Color.FromArgb(255, 130, 113);
                        (e.Row.FindControl("btnVerifiedPDF") as Button).Visible = false;
                    }
                    else
                    {
                        e.Row.BackColor = Color.White;
                        (e.Row.FindControl("btnVerifiedPDF") as Button).Visible = false;
                    }
                }
            //}
            //else
            //{
            //    isMobileOrNID = false;
            //}
            //string strStatus = e.Row.Cells[14].Text;

            //if (strStatus.Equals("000"))
            //{
            //    Button btnSuccess = (Button)e.Row.FindControl("Button1");
            //    btnSuccess.Enabled = false;
            //    Button btnReverse = (Button)e.Row.FindControl("Button2");
            //    btnReverse.Enabled = false;
            //}
        }
    }
    protected void GetDataMessage()
    {
        //string strSQL = "SELECT * FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y'";
        //DataSet dsAll = objServiceHandler.ExecuteQuery(strSQL);
        //strSQL = "SELECT * FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' AND IS_UPDATE = 'N' AND REMARKS IS NULL";
        //DataSet dsNonVerified = objServiceHandler.ExecuteQuery(strSQL);
        //strSQL = "SELECT * FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' AND IS_UPDATE = 'N' AND TRIM(REMARKS) IS NOT NULL";
        //DataSet dsCancel = objServiceHandler.ExecuteQuery(strSQL);
        //strSQL = "SELECT * FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' AND IS_UPDATE = 'Y'";
        //DataSet dsVerified = objServiceHandler.ExecuteQuery(strSQL);
        //strSQL = "SELECT * FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' AND IS_PROCESSING = 'Y'";
        //DataSet dsOnProcess = objServiceHandler.ExecuteQuery(strSQL);

        string strSQL = "SELECT (SELECT COUNT(*) FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' OR IS_PROCESSING IN ('C','CWS')) TOTAL_REGISTER, (SELECT COUNT(*) FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' AND IS_UPDATE = 'N' AND REMARKS IS NULL) PENDING, (SELECT COUNT(*) FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'E' AND IS_UPDATE = 'N' AND IS_PROCESSING IN ('C','CWS')) CANCELS, (SELECT COUNT(*) FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' AND IS_UPDATE = 'Y') VERIFIED, (SELECT COUNT(*) FROM DIGITAL_KYC_INFO WHERE IS_REGISTER = 'Y' AND IS_PROCESSING = 'Y') PROCESSING  FROM DUAL";
        DataSet ds = objServiceHandler.ExecuteQuery(strSQL);
        string totalData = ds != null ? ds.Tables[0].Rows[0]["TOTAL_REGISTER"].ToString() : "0";
        string non = ds != null ? ds.Tables[0].Rows[0]["PENDING"].ToString() : "0";
        string cancel = ds != null ? ds.Tables[0].Rows[0]["CANCELS"].ToString() : "0";
        string verified = ds != null ? ds.Tables[0].Rows[0]["VERIFIED"].ToString() : "0";
        string onProcess = ds != null ? ds.Tables[0].Rows[0]["PROCESSING"].ToString() : "0";
        switch (ddlSearchOption.SelectedValue)
        {
            case "verified":
                lblCountMsg.Text = "Total Reg: " + totalData
            + ", Pending Total: " + non
             + ", Cancelled Total: " + cancel
              + ", Verified Total: " + verified
               + ", On Process: " + onProcess;
                break;
            case "cancel":
                lblCountMsg.Text = "Total Reg: " + totalData
            + ", Pending Total: " + non
             + ", Cancelled Total: " + cancel
              + ", Verified Total: " + verified
               + ", On Process: " + onProcess;
                break;
            case "onproc":
                lblCountMsg.Text = "Total Reg: " + totalData
            + ", Pending Total: " + non
             + ", Cancelled Total: " + cancel
              + ", Verified Total: " + verified
               + ", On Process: " + onProcess;
                break;
            case "all":
                lblCountMsg.Text = "Total Reg: " + totalData
            + ", Pending Total: " + non
             + ", Cancelled Total: " + cancel
              + ", Verified Total: " + verified
               + ", On Process: " + onProcess;
                break;
            default:
                lblCountMsg.Text = "Total Reg: " + totalData
            + ", Pending Total: " + non
             + ", Cancelled Total: " + cancel
              + ", Verified Total: " + verified
               + ", On Process: " + onProcess;
                break;
        }
        
    }
}
