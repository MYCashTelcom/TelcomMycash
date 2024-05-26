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

public partial class COMMON_frmIsoRequestStatus : System.Web.UI.Page
{

    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    string strUserName = string.Empty;
    string strPassword = string.Empty;
    private DateTime dateTime = DateTime.Now;

    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            
            if (dtpFromDate.DateString != "")
            {
                dtpFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dateTime.AddMinutes(-30));
                //dtpFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dateTime.AddDays(-30));
                //dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dateTime.AddDays(-29));
            }

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

            
            LoadGrid();
            panelTitle.Visible = false;
        }
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
    protected void btnShow_Click(object sender, EventArgs e)
    {
        panelTitle.Visible = true;
        grdIsoRequest.Visible = true;

        LoadGrid();

        #region Old Code

        //string strSql = "";
        //string strOwnerCode = ddlOwnerCode.SelectedValue;
        //string strRequestStatus = ddlReqStatus.SelectedValue;
        //string strServiceListProcess = ddlServiceListProcess.SelectedValue;

        //if (ddlReqStatus.SelectedValue == "A")
        //{
        //    try
        //    {
        //        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
        //               + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
        //               + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
        //               + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE ISO_REQUEST_CODE = '" + strServiceListProcess + "' AND"
        //               + "  ISO_OWNER_CODE = '" + strOwnerCode + "' "
        //               + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
        //               + " ORDER BY ISO_REQUEST_TIME DESC";

        //        //BETWEEN TO_DATE(\'" + dptFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')

        //        sdsISOReq.SelectCommand = strSql;
        //        sdsISOReq.DataBind();
        //        grdIsoRequest.DataBind();
        //    }
        //    catch (Exception ex )
        //    {
        //        ex.Message.ToString();
        //    }
        //}

        //else
        //{
        //    try
        //    {
        //        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
        //               + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
        //               + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
        //               + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE  ISO_REQUEST_CODE = '"+ddlServiceListProcess.SelectedValue+"' AND "
        //               + " ISO_REQUEST_STATUS = '" + strRequestStatus + "' AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
        //               + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
        //               + " ORDER BY ISO_REQUEST_TIME DESC";

        //        sdsISOReq.SelectCommand = strSql;
        //        sdsISOReq.DataBind();
        //        grdIsoRequest.DataBind();
        //    }
        //    catch (Exception exception)
        //    {
        //        exception.Message.ToString();
        //    }
        //}

#endregion
    }

    private void LoadGrid()
    {
        string strSql = "";
        string strOwnerCode = ddlOwnerCode.SelectedValue;
        string strRequestStatus = ddlReqStatus.SelectedValue;
        string strServiceListProcess = ddlServiceListProcess.SelectedValue;

        try
        {
            if (chkBoxAllSrv.Checked == true)
            {
                ddlServiceListProcess.Enabled = false;

                if (ddlReqStatus.SelectedValue == "A")
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS,"
                           + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                           + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                           + " REQUEST_ID, ISO_REQUEST_INSERT_TIME  FROM   ISO_REQUEST WHERE ISO_OWNER_CODE = '" + strOwnerCode + "'  "
                           + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                           + " ORDER BY ISO_REQUEST_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();
                }

                else if (ddlReqStatus.SelectedValue == "S" || ddlReqStatus.SelectedValue == "F")
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                             + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                             + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                             + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM ISO_REQUEST WHERE  ISO_REQUEST_STATUS = '" + strRequestStatus + "' "
                             + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                             + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                             + " AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                             + " ORDER BY ISO_REQUEST_TIME DESC ";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();
                }

                else
                {
                    lblMsg.Text = "";
                }
            }

            else if (chkBoxAllSrv.Checked == false)
            {
                ddlServiceListProcess.Enabled = true;

                if (ddlReqStatus.SelectedValue == "A")
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                         + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                         + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                         + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE ISO_REQUEST_CODE = '" + ddlServiceListProcess.SelectedValue + "' "
                         + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' AND TRUNC(ISO_REQUEST_INSERT_TIME) "
                         + " BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                         + " ORDER BY ISO_REQUEST_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();
                }

                else if (ddlReqStatus.SelectedValue == "S" || ddlReqStatus.SelectedValue == "F")
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                           + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                           + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                           + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE  ISO_REQUEST_CODE = '" + ddlServiceListProcess.SelectedValue + "' "
                           + " AND ISO_REQUEST_STATUS = '" + strRequestStatus + "' AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                           + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                           + " ORDER BY ISO_REQUEST_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();

                }


            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();

        }

        #region old Code
        //if (ddlReqStatus.SelectedValue == "A")
        //{
        //    try
        //    {
        //        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
        //               + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
        //               + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
        //               + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE ISO_REQUEST_CODE = '" + strServiceListProcess + "' AND"
        //               + "  ISO_OWNER_CODE = '" + strOwnerCode + "' "
        //               + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
        //               + " ORDER BY ISO_REQUEST_TIME DESC";

        //        sdsISOReq.SelectCommand = strSql;
        //        sdsISOReq.DataBind();
        //        grdIsoRequest.DataBind();
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }

        //}

        //else
        //{
        //    try
        //    {
        //        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
        //               + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
        //               + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
        //               + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE  ISO_REQUEST_CODE = '" + strServiceListProcess + "' AND "
        //               + " ISO_REQUEST_STATUS = '" + strRequestStatus + "' AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
        //               + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
        //               + " ORDER BY ISO_REQUEST_TIME DESC";

        //        sdsISOReq.SelectCommand = strSql;
        //        sdsISOReq.DataBind();
        //        grdIsoRequest.DataBind();
        //    }
        //    catch (Exception exception)
        //    {
        //        exception.Message.ToString();
        //    }
        //}
#endregion
    }

    protected void grdIsoRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdIsoRequest.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void grdIsoRequest_PageIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void ddlReqStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    
    protected void chkBoxAllSrv_CheckedChanged(object sender, EventArgs e)
    {
        LoadGrid();

        #region old Code

        //if (chkBoxAllSrv.Checked == true)
        //{
        //    string strSql = "";
        //    string strOwnerCode = ddlOwnerCode.SelectedValue;
        //    string strRequestStatus = ddlReqStatus.SelectedValue;
        //    ddlServiceListProcess.Enabled = false;

        //    try
        //    {
        //        if (ddlReqStatus.SelectedValue == "A")
        //        {
        //            strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
        //                 + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
        //                 + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
        //                 + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM ISO_REQUEST WHERE ISO_OWNER_CODE = '" + strOwnerCode + "' "
        //                 + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
        //                 + " ORDER BY ISO_REQUEST_TIME DESC";

        //            sdsISOReq.SelectCommand = strSql;
        //            sdsISOReq.DataBind();
        //            grdIsoRequest.DataBind();
        //        }

        //        else if (ddlReqStatus.SelectedValue == "S" || ddlReqStatus.SelectedValue == "F")
        //        {
        //            strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
        //                       + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
        //                       + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
        //                       + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST "
        //                       + " WHERE ISO_REQUEST_STATUS = '" + strRequestStatus + "' AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
        //                       + " AND TRUNC(ISO_REQUEST_INSERT_TIME) BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
        //                       + " ORDER BY ISO_REQUEST_TIME DESC";

        //            sdsISOReq.SelectCommand = strSql;
        //            sdsISOReq.DataBind();
        //            grdIsoRequest.DataBind();
        //        }
        //        else
        //        {
        //            lblMsg.Text = "";
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        lblMsg.Text = exception.Message.ToString();
        //    }
        //}

        //else
        //{
        //    ddlServiceListProcess.Enabled = true;
        //}

        #endregion 
    }

    protected void ddlServiceListProcess_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void ddlOwnerCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
}
