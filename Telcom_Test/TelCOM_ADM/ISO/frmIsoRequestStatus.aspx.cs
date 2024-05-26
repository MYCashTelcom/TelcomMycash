using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class ISO_frmIsoRequestStatus : System.Web.UI.Page
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
                           + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                           + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();
                }

                else if (ddlReqStatus.SelectedValue == "S" )
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                             + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                             + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                             + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM ISO_REQUEST WHERE  ISO_REQUEST_STATUS = '" + strRequestStatus + "' "
                             + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                             + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                             + " AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                             + " ORDER BY ISO_REQUEST_INSERT_TIME DESC ";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();
                }

                else if (ddlReqStatus.SelectedValue == "F")
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                             + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                             + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                             + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM ISO_REQUEST WHERE  (ISO_RESPONSE_CODE NOT IN ('00') OR ISO_RESPONSE_CODE IS NULL) "
                             + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                             + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                             + " AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                             + " ORDER BY ISO_REQUEST_INSERT_TIME DESC ";

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
                         + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' AND ISO_REQUEST_INSERT_TIME "
                         + " BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                         + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();
                }


                else if (ddlReqStatus.SelectedValue == "F" && ddlServiceListProcess.SelectedValue == "010000")
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                           + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                           + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                           + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE ISO_REQUEST_TYPE = 'HTTP' "
                           + " AND ( ISO_REQUEST_STATUS IN ('F') OR  ISO_REQUEST_STATUS IS NULL) AND ISO_OWNER_CODE = '"+ strOwnerCode +"' "
                           + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                           + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();

                }


                else if (ddlReqStatus.SelectedValue == "S" )
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                           + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                           + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                           + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE  ISO_REQUEST_CODE = '" + ddlServiceListProcess.SelectedValue + "' "
                           + " AND ISO_REQUEST_STATUS = '" + strRequestStatus + "' AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                           + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                           + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();

                }

                else if ( ddlReqStatus.SelectedValue == "F")
                {
                    panelTitle.Visible = true;
                    grdIsoRequest.Visible = true;

                    strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                           + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                           + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                           + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE  ISO_REQUEST_CODE = '" + ddlServiceListProcess.SelectedValue + "' "
                           + " AND (ISO_RESPONSE_CODE NOT IN ('00') OR ISO_RESPONSE_CODE IS NULL) AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                           + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                           + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";

                    sdsISOReq.SelectCommand = strSql;
                    sdsISOReq.DataBind();
                    grdIsoRequest.DataBind();

                }


                else
                { 
                    // do nothing
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
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
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
                               + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                               + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";

                        
                    }

                    else if (ddlReqStatus.SelectedValue == "S" )
                    {
                        panelTitle.Visible = true;
                        grdIsoRequest.Visible = true;

                        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                                 + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                                 + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                                 + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM ISO_REQUEST WHERE  ISO_REQUEST_STATUS = '" + strRequestStatus + "' "
                                 + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                                 + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                                 + " AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                                 + " ORDER BY ISO_REQUEST_INSERT_TIME DESC ";                        
                    }

                    else if ( ddlReqStatus.SelectedValue == "F")
                    {
                        panelTitle.Visible = true;
                        grdIsoRequest.Visible = true;

                        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                                 + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                                 + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                                 + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM ISO_REQUEST WHERE (ISO_RESPONSE_CODE NOT IN ('00') OR ISO_RESPONSE_CODE IS NULL) "
                                 + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                                 + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                                 + " AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                                 + " ORDER BY ISO_REQUEST_INSERT_TIME DESC ";
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
                             + " AND ISO_OWNER_CODE = '" + strOwnerCode + "' AND ISO_REQUEST_INSERT_TIME "
                             + " BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                             + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";

                        
                    }


                    else if (ddlReqStatus.SelectedValue == "F" && ddlServiceListProcess.SelectedValue == "010000")
                    {
                        panelTitle.Visible = true;
                        grdIsoRequest.Visible = true;

                        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                               + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                               + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                               + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE ISO_REQUEST_TYPE = 'HTTP' "
                               + " AND ( ISO_REQUEST_STATUS IN ('F') OR  ISO_REQUEST_STATUS IS NULL) AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                               + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                               + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";                       

                    }

                    else if (ddlReqStatus.SelectedValue == "S" )
                    {
                        panelTitle.Visible = true;
                        grdIsoRequest.Visible = true;

                        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                               + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                               + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                               + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE  ISO_REQUEST_CODE = '" + ddlServiceListProcess.SelectedValue + "' "
                               + " AND ISO_REQUEST_STATUS = '" + strRequestStatus + "' AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                               + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                               + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";
                    }

                    else if (ddlReqStatus.SelectedValue == "F")
                    {
                        panelTitle.Visible = true;
                        grdIsoRequest.Visible = true;

                        strSql = " SELECT DISTINCT ISO_REQ_ID, ISO_REQUEST_PARTY, ISO_RECEIPENT_PARTY, ISO_REQUEST_TIME, ISO_REQ_PROCESS_STATUS, "
                               + " ISO_REQUEST_CODE, ISO_REQUEST_STATUS, ISO_RESPONSE_CODE, ISO_RESPONSE_TIME, ISO_CLIENT_REQ_ID, "
                               + " ISO_EXCEPTION, ISO_OWNER_CODE, ISO_REQUEST_TYPE, ISO_REQ_AMOUNT, HTTP_RESPONSE, HTTP_RESPONSE_CODE, "
                               + " REQUEST_ID, ISO_REQUEST_INSERT_TIME FROM   ISO_REQUEST WHERE  ISO_REQUEST_CODE = '" + ddlServiceListProcess.SelectedValue + "' "
                               + " AND (ISO_RESPONSE_CODE NOT IN ('00') OR ISO_RESPONSE_CODE IS NULL) AND ISO_OWNER_CODE = '" + strOwnerCode + "' "
                               + " AND ISO_REQUEST_INSERT_TIME BETWEEN TO_DATE(\'" + dtpFromDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\') "
                               + " ORDER BY ISO_REQUEST_INSERT_TIME DESC";
                    }

                    else
                    { 
                        // do nothing
                    }


                }


                string strHTML = "", fileName = "";
                lblMsg.Text = "";

                DataSet dtsAccount = new DataSet();
                fileName = "ISO_req_ sta_Rpt";
                //------------------------------------------Report File xl processing   -------------------------------------

                dtsAccount = objServiceHandler.ExecuteQuery(strSql);

                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>Mercantile Bank Limited</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
                strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>MYCash</h3></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>ISO Request Status Report</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
                strHTML = strHTML + "<tr><td COLSPAN=19 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Date Range: '" + dtpFromDate.DateString + "' To '" + dtpFromDate.DateString + "'</h2></td></tr>";
                strHTML = strHTML + "</table>";
                strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
                strHTML = strHTML + "<tr>";

                strHTML = strHTML + "<td valign='middle' >Sl</td>";
                strHTML = strHTML + "<td valign='middle' >ISO Request Id</td>";
                strHTML = strHTML + "<td valign='middle' >ISO Request Party </td>";
                strHTML = strHTML + "<td valign='middle' >ISO Recipient Party </td>";
                strHTML = strHTML + "<td valign='middle' >Request Time</td>";
                strHTML = strHTML + "<td valign='middle' >Request Code</td>";
                strHTML = strHTML + "<td valign='middle' >Request Status</td>";
                strHTML = strHTML + "<td valign='middle' >Response Code</td>";
                strHTML = strHTML + "<td valign='middle' >Response Time</td>";
                strHTML = strHTML + "<td valign='middle' >Client Request Id</td>";
                strHTML = strHTML + "<td valign='middle' >Exception </td>";
                strHTML = strHTML + "<td valign='middle' >ISO Owner Code </td>";
                strHTML = strHTML + "<td valign='middle' >Request Type</td>";
                strHTML = strHTML + "<td valign='middle' >Request Process Status</td>";
                strHTML = strHTML + "<td valign='middle' >Amount</td>";
                strHTML = strHTML + "<td valign='middle' >Http Response</td>";
                strHTML = strHTML + "<td valign='middle' >Http Response Code</td>";
                strHTML = strHTML + "<td valign='middle' >Request Id</td>";
                strHTML = strHTML + "<td valign='middle' >Request Insert Time </td>";
                strHTML = strHTML + "</tr>";


                if (dtsAccount.Tables[0].Rows.Count > 0)
                {
                    int SerialNo = 1;
                    foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                    {
                        strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQ_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQUEST_PARTY"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_RECEIPENT_PARTY"].ToString() + " </td>";
                        //strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ISO_REQUEST_TIME"].ToString())) + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQUEST_TIME"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQUEST_CODE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQUEST_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_RESPONSE_CODE"].ToString() + " </td>";
                        //strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ISO_RESPONSE_TIME"].ToString())) + " </td>";
                        if (prow["ISO_RESPONSE_TIME"].ToString() != "")
                        {
                            strHTML = strHTML + " <td > '" + String.Format("{0:dd-MMM-yyyy}", Convert.ToDateTime(prow["ISO_RESPONSE_TIME"].ToString())) + " </td>";
                        }
                        else
                        {
                            string strNull = "";
                            strHTML = strHTML + " <td > '" + strNull + " </td>";
                        }

                        strHTML = strHTML + " <td > '" + prow["ISO_CLIENT_REQ_ID"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_EXCEPTION"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_OWNER_CODE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQUEST_TYPE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQ_PROCESS_STATUS"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQ_AMOUNT"].ToString() + " </td>";

                        strHTML = strHTML + " <td > '" + prow["HTTP_RESPONSE"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["HTTP_RESPONSE_CODE"].ToString() + "</td>";
                        strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                        strHTML = strHTML + " <td > '" + prow["ISO_REQUEST_INSERT_TIME"].ToString() + "</td>";
                        strHTML = strHTML + " </tr> ";
                        SerialNo = SerialNo + 1;

                    }
                }

                strHTML = strHTML + "<tr>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";
                strHTML = strHTML + " <td > " + "" + " </td>";

                strHTML = strHTML + " </tr>";
                strHTML = strHTML + " </table>";

                clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");

                lblMsg.ForeColor = Color.White;
                lblMsg.Text = "Report Generated Successfully...";


            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();

            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
