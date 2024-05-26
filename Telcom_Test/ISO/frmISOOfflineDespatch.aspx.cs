﻿using System;
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

public partial class ISO_frmISOOfflineDespatch : System.Web.UI.Page
{

    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    clsServiceHandler objReport = new clsServiceHandler();
    //clsLogWritter objLogWriter = new clsLogWritter();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMsg.Visible = false;
        if (!IsPostBack)
        {
            try
            {
                DateTime dt = DateTime.Now;
                if (dptFromDate.DateString != "")
                {
                    dptFromDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt.AddDays(-1));
                    dtpToDate.DateString = String.Format("{0:dd-MMM-yyyy HH:mm:ss}", dt);
                    LoadDropDownData();
                }
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch (Exception ex)
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
            }
        }
       // LoadRequestList();
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

    private void LoadDropDownData()
    {
        string strSQL = "";
        strSQL = "SELECT DISTINCT VENDOR_CODE FROM BDMIT_ERP_101.BANK_BRANCH_DETAIL";
        try
        {
            sdsVendorType.SelectCommand = strSQL;
            sdsVendorType.DataBind();
            ddlVendorType.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }

    private void DespatchTemenos()
    {
        /*

        DataSet dtsOffline = new DataSet();
        string strHTML = "", fileName = "", strSQL;
        lblMsg.Text = "";
        fileName = "Offline_Temenos_Report";
        strSQL = " SELECT distinct CDT.*,BBD.BANK_BRANCH_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
               + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
               + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' "
               + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedItem.Text + "'"
               + "  AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtSubscriberNo.Text.Equals(""))
        {
            strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
        }
        //if (!txtServiceCode.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        //}
        strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";
        try
        {
            dtsOffline = objReport.GetOfflineData(strSQL);
            SaveAuditInfo("Preview", "ISO Offline Request Report");
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + "<td style='font-size:16px;font-weight:bold;' > Debit Account</td>";
            strHTML = strHTML + "<td style='font-size:16px;font-weight:bold;' >Installment Amount</td>";
            strHTML = strHTML + "<td style='font-size:16px;font-weight:bold;' >Credit Account</td>";
            strHTML = strHTML + "<td style='font-size:16px;font-weight:bold;' >Branch Code</td>";
            strHTML = strHTML + "<td style='font-size:16px;font-weight:bold;' >Remarks</td>";
            strHTML = strHTML + "</tr>";

            if (dtsOffline.Tables["BDMIT_ERP_101.CAS_DPS_TRANSACTION"].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow pRow in dtsOffline.Tables["BDMIT_ERP_101.CAS_DPS_TRANSACTION"].Rows)
                {
                    string strMbl = objReport.GetMbl(pRow["CAS_ACC_ID"].ToString());
                    string strDate = pRow["CAS_TRAN_DATE"].ToString();
                    string strRemarks = "";
                    strRemarks = strMbl + " " + strDate;
                    if (strRemarks.ToString().Length > 35)
                    {
                        strRemarks = strRemarks.Substring(0, 35);
                    }

                    strHTML = strHTML + "<tr>";
                    strHTML = strHTML + " <td> '00000000001 </td>";
                    strHTML = strHTML + " <td> '" + pRow["CAS_TRAN_AMT"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + pRow["DPS_REF_CODE"].ToString() + "L </td>";
                    strHTML = strHTML + " <td > 'BD001" + pRow["BANK_BRANCH_CODE"].ToString() + " </td>";
                    strHTML = strHTML + " <td >" + strRemarks + " </td>";
                    strHTML = strHTML + " </tr> ";
                    SerialNo = SerialNo + 1;
                    string strDpsID = pRow["CAS_DPS_ID"].ToString();
                    objReport.updateDPS(strDpsID, "Y", DateTime.Now.ToString());
                }
            }
            LoadRequestList();
            strHTML = strHTML + "<tr>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " <td > " + "" + " </td>";
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
          
          
         */ 
    }
    
    private void DespatchPCBank()
    {
        /*

        DataSet dtsOffline = new DataSet();
        string strSQL = "";
        strSQL = " SELECT distinct CDT.*,IR.ISO_RESPONSE_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
               + " APSNG101.ISO_REQUEST IR,BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD WHERE CDT.CAS_DPS_ID=IR.ISO_CLIENT_REQ_ID AND "
               + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' "
               + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedItem.Text + "'"
               + "  AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString
               + "\',\'dd/mm/yyyy HH24:MI:SS\')";
        if (!txtRequestParty.Text.Equals(""))
        {
            strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
        }
        if (!txtSubscriberNo.Text.Equals(""))
        {
            strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
        }
        //if (!txtServiceCode.Text.Equals(""))
        //{
        //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
        //}
        strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";
        dtsOffline = objReport.GetOfflineData(strSQL);
        try
        {
            string strTotalCount = "";
            foreach (DataRow pRow in dtsOffline.Tables["BDMIT_ERP_101.CAS_DPS_TRANSACTION"].Rows)
            {
                int intCunt = 0;
                string strDpsID = pRow["CAS_DPS_ID"].ToString();
                string strReqID = pRow["CAS_DPS_ID"].ToString();
                string strAmount = pRow["CAS_TRAN_AMT"].ToString();
                string strAccountNo = pRow["DPS_REF_CODE"].ToString();
                string strDate = pRow["CAS_TRAN_DATE"].ToString();
                DateTime dt = DateTime.Parse(strDate);
                string strGetDate = dt.ToString("dd-MMM-yyyy");
                string strMsg = "Offline Transaction";
                string strMbl = objReport.GetMbl(pRow["CAS_ACC_ID"].ToString());
                string strFile = "|" + strDpsID + "|" + strGetDate + "|" + strMbl.Substring(0, 11) + "|" + strAccountNo + "|" + strMsg + "|" + strAmount + "|" + strReqID + "|" + Environment.NewLine;

                strTotalCount = strFile + strTotalCount;
                //objLogWriter.WritLogISOOffline(DateTime.Now.ToLongTimeString() + ":" + strDpsID + "|" + strGetDate + "|" + strMbl.Substring(0, 11) + "|" + strAccountNo + "|" + strMsg + "|" + strAmount + "|" + strReqID + Environment.NewLine);

                objReport.updateDPS(strDpsID, "Y", DateTime.Now.ToString());
            }
            LoadRequestList();
            try
            {
                clsGridExport.ExportToMSExcel("ISO_Offline_Trans", "txt", strTotalCount, "landscape");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
         
         */ 
         
    }
    
    
    public void LoadRequestList()
    {
        string strSQL = "";
        try
        {
            if (ddlVendorType.SelectedValue.ToString() == "PC_BANK")
            {

                strSQL = " SELECT CDT.CAS_DPS_ID  CAS_DPS_ID, CDT.REQUEST_ID REQUEST_ID, CDT.CAS_TRAN_DATE CAS_TRAN_DATE, "
                       + " CDT.CAS_TRAN_AMT CAS_TRAN_AMT, CDT.DPS_REF_CODE DPS_REF_CODE, CDT.UPLOAD_STATUS UPLOAD_STATUS, "
                       + " CDT.DPS_OWNER DPS_OWNER, CDT.CAS_ISO_REQ_STATUS CAS_ISO_REQ_STATUS, CDT.CAS_ISO_REQ_DESPATCH CAS_ISO_REQ_DESPATCH " 
                       + " FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                       + " APSNG101.ISO_REQUEST IR,BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE CDT.CAS_DPS_ID=IR.ISO_CLIENT_REQ_ID AND "
                       + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' AND CDT.TRAN_TYPE IS NULL"
                       + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                       + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                       + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

                /*
                strSQL = " SELECT CDT.*,IR.ISO_RESPONSE_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                       + " APSNG101.ISO_REQUEST IR,BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE CDT.CAS_DPS_ID=IR.ISO_CLIENT_REQ_ID AND "
                       + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' "
                       + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                       + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                       + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";  */
                if (!txtRequestParty.Text.Equals(""))
                {
                    strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
                }
                if (!txtSubscriberNo.Text.Equals(""))
                {
                    strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
                }
                //if (!txtServiceCode.Text.Equals(""))
                //{
                //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
                //}
                strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";


            }
            else
            {
                strSQL = " SELECT CDT.CAS_DPS_ID  CAS_DPS_ID, CDT.REQUEST_ID REQUEST_ID, CDT.CAS_TRAN_DATE CAS_TRAN_DATE, "
                       + " CDT.CAS_TRAN_AMT CAS_TRAN_AMT, CDT.DPS_REF_CODE DPS_REF_CODE, CDT.UPLOAD_STATUS UPLOAD_STATUS, "
                       + " CDT.DPS_OWNER DPS_OWNER, CDT.CAS_ISO_REQ_STATUS CAS_ISO_REQ_STATUS, CDT.CAS_ISO_REQ_DESPATCH CAS_ISO_REQ_DESPATCH "
                       + " FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' AND CDT.TRAN_TYPE IS NULL"
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";
                
                /*
                strSQL = " SELECT CDT.*,BBD.BANK_BRANCH_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' "
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')"; */
                if (!txtRequestParty.Text.Equals(""))
                {
                    strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
                }
                if (!txtSubscriberNo.Text.Equals(""))
                {
                    strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
                }
                //if (!txtServiceCode.Text.Equals(""))
                //{
                //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
                //}
                strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";
            }

            sdsOffline.SelectCommand = strSQL;
            sdsOffline.DataBind();
            gdvOffline.DataBind();
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        try
        {
            //objSysAdmin.SetSeessionData(Session["Branch_ID"].ToString());
            string IPAddress = Request.ServerVariables["remote_addr"];
            string Technology = Request.Browser.Browser + Request.Browser.Version;
            string IPTechnology = IPAddress + "-" + Technology;
            objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
            Session.Clear();
            Response.Redirect("../frmSeesionExpMesage.aspx");
        }
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gdvOffline.Visible = true;
        gdvOfflineDSPView.Visible = false;
        LoadRequestList();
    }
    
    protected void btnDespatchView_Click(object sender, EventArgs e)
    {
        gdvOfflineDSPView.Visible = true;
        gdvOffline.Visible = false;
        DespatchDataView();
    }

    private void DespatchDataView()
    {
        string strSQL = "";
        try
        {

            strSQL = " SELECT CDT.CAS_DPS_ID  CAS_DPS_ID, CDT.REQUEST_ID REQUEST_ID, CDT.CAS_TRAN_DATE CAS_TRAN_DATE, "
                       + " CDT.CAS_TRAN_AMT CAS_TRAN_AMT, CDT.DPS_REF_CODE DPS_REF_CODE, CDT.UPLOAD_STATUS UPLOAD_STATUS, "
                       + " CDT.DPS_OWNER DPS_OWNER, CDT.CAS_ISO_REQ_STATUS CAS_ISO_REQ_STATUS, CDT.CAS_ISO_REQ_DESPATCH CAS_ISO_REQ_DESPATCH "
                       + " FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='S' AND CAS_ISO_REQ_DESPATCH='Y' AND CDT.TRAN_TYPE IS NULL"
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

            /*
            strSQL = " SELECT CDT.*,BBD.BANK_BRANCH_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='S' AND CAS_ISO_REQ_DESPATCH='Y' "
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')"; */
            if (!txtRequestParty.Text.Equals(""))
            {
                strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
            }
            if (!txtSubscriberNo.Text.Equals(""))
            {
                strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
            }
            //if (!txtServiceCode.Text.Equals(""))
            //{
            //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
            //}
            strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";

            sdsOffLineDespatch.SelectCommand = strSQL;
            sdsOffLineDespatch.DataBind();
            gdvOfflineDSPView.DataBind();
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gdvOffline_PageIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void gdvOfflineDSPView_PageIndexChanged(object sender, EventArgs e)
    {
        DespatchDataView();
    }
    protected void btnExportNDES_Click(object sender, EventArgs e)
    {
        string strSQL = "";
        try
        {
            if (ddlVendorType.SelectedValue.ToString() == "PC_BANK")
            {

                strSQL = " SELECT CDT.CAS_DPS_ID  CAS_DPS_ID, CDT.REQUEST_ID REQUEST_ID, CDT.CAS_TRAN_DATE CAS_TRAN_DATE, "
                       + " CDT.CAS_TRAN_AMT CAS_TRAN_AMT, CDT.DPS_REF_CODE DPS_REF_CODE, CDT.UPLOAD_STATUS UPLOAD_STATUS, "
                       + " CDT.DPS_OWNER DPS_OWNER, CDT.CAS_ISO_REQ_STATUS CAS_ISO_REQ_STATUS, CDT.CAS_ISO_REQ_DESPATCH CAS_ISO_REQ_DESPATCH "
                       + " FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                       + " APSNG101.ISO_REQUEST IR,BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE CDT.CAS_DPS_ID=IR.ISO_CLIENT_REQ_ID AND "
                       + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' AND CDT.TRAN_TYPE IS NULL"
                       + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                       + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                       + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

                /*
                strSQL = " SELECT CDT.*,IR.ISO_RESPONSE_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                       + " APSNG101.ISO_REQUEST IR,BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE CDT.CAS_DPS_ID=IR.ISO_CLIENT_REQ_ID AND "
                       + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' "
                       + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                       + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                       + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";  */
                if (!txtRequestParty.Text.Equals(""))
                {
                    strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
                }
                if (!txtSubscriberNo.Text.Equals(""))
                {
                    strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
                }
                //if (!txtServiceCode.Text.Equals(""))
                //{
                //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
                //}
                strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";


            }
            else
            {
                strSQL = " SELECT CDT.CAS_DPS_ID  CAS_DPS_ID, CDT.REQUEST_ID REQUEST_ID, CDT.CAS_TRAN_DATE CAS_TRAN_DATE, "
                       + " CDT.CAS_TRAN_AMT CAS_TRAN_AMT, CDT.DPS_REF_CODE DPS_REF_CODE, CDT.UPLOAD_STATUS UPLOAD_STATUS, "
                       + " CDT.DPS_OWNER DPS_OWNER, CDT.CAS_ISO_REQ_STATUS CAS_ISO_REQ_STATUS, CDT.CAS_ISO_REQ_DESPATCH CAS_ISO_REQ_DESPATCH "
                       + " FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' AND CDT.TRAN_TYPE IS NULL"
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

                /*
                strSQL = " SELECT CDT.*,BBD.BANK_BRANCH_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='O' AND CAS_ISO_REQ_DESPATCH='N' "
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')"; */
                if (!txtRequestParty.Text.Equals(""))
                {
                    strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
                }
                if (!txtSubscriberNo.Text.Equals(""))
                {
                    strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
                }
                //if (!txtServiceCode.Text.Equals(""))
                //{
                //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
                //}
                strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";
            }

            gdvOfflineDSPView.Visible = false;
            gdvOffline.Visible = false;


            // ---- for excel file----

            clsServiceHandler objServiceHandler = new clsServiceHandler();
            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "NotDespatched";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSQL);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>List of Not Despatched Transaction</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Print Date (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:12px;font-weight:bold;'><h3 align=center>Date Range Between " + String.Format("{0:dd-MMM-yyyy}", dptFromDate.DateString) + " and " + String.Format("{0:dd-MMM-yyyy}", dtpToDate.DateString) + "</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >DPS ID</td>";
            strHTML = strHTML + "<td valign='middle' >Request ID</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >MSS Account</td>";
            strHTML = strHTML + "<td valign='middle' >Upload Status</td>";
            strHTML = strHTML + "<td valign='middle' >DPS Owner</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Request Status</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Request Despatch</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_DPS_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_REF_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UPLOAD_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_OWNER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ISO_REQ_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ISO_REQ_DESPATCH"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "NotDespatched_file_generate");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }
    protected void btnDSPExport_Click(object sender, EventArgs e)
    {
        string strSQL = "";
        try
        {
            strSQL = " SELECT CDT.CAS_DPS_ID  CAS_DPS_ID, CDT.REQUEST_ID REQUEST_ID, CDT.CAS_TRAN_DATE CAS_TRAN_DATE, "
                       + " CDT.CAS_TRAN_AMT CAS_TRAN_AMT, CDT.DPS_REF_CODE DPS_REF_CODE, CDT.UPLOAD_STATUS UPLOAD_STATUS, "
                       + " CDT.DPS_OWNER DPS_OWNER, CDT.CAS_ISO_REQ_STATUS CAS_ISO_REQ_STATUS, CDT.CAS_ISO_REQ_DESPATCH CAS_ISO_REQ_DESPATCH "
                       + " FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='S' AND CAS_ISO_REQ_DESPATCH='Y' AND CDT.TRAN_TYPE IS NULL"
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')";

            /*
            strSQL = " SELECT CDT.*,BBD.BANK_BRANCH_CODE FROM BDMIT_ERP_101.CAS_DPS_TRANSACTION CDT,"
                        + " BDMIT_ERP_101.BANK_BRANCH_DETAIL BBD  WHERE "
                        + " DPS_OWNER='MBL'  AND CAS_ISO_REQ_STATUS='S' AND CAS_ISO_REQ_DESPATCH='Y' "
                        + " AND SUBSTR(CDT.DPS_REF_CODE,1,4)=BBD.BANK_BRANCH_CODE AND BBD.VENDOR_CODE='" + ddlVendorType.SelectedValue + "'"
                        + " AND CAS_TRAN_DATE BETWEEN TO_DATE(\'" + dptFromDate.DateString
                        + "\',\'dd/mm/yyyy HH24:MI:SS\') AND TO_DATE(\'" + dtpToDate.DateString + "\',\'dd/mm/yyyy HH24:MI:SS\')"; */
            if (!txtRequestParty.Text.Equals(""))
            {
                strSQL = strSQL + " AND CAS_ACC_ID LIKE '%" + txtRequestParty.Text + "%'";
            }
            if (!txtSubscriberNo.Text.Equals(""))
            {
                strSQL = strSQL + " AND DPS_REF_CODE LIKE '%" + txtSubscriberNo.Text + "%'";
            }
            //if (!txtServiceCode.Text.Equals(""))
            //{
            //    strSQL = strSQL + " AND UPPER(SUBSTR(SR.REQUEST_TEXT,1,INSTR(SR.REQUEST_TEXT,'*',2)-1)) LIKE UPPER('%" + txtServiceCode.Text + "%')";
            //}
            strSQL = strSQL + " ORDER BY CAS_TRAN_DATE DESC";


            gdvOfflineDSPView.Visible = false;
            gdvOffline.Visible = false;


            // ---- for excel file----

            clsServiceHandler objServiceHandler = new clsServiceHandler();
            string strHTML = "", fileName = "";
            lblMsg.Text = "";

            DataSet dtsAccount = new DataSet();
            fileName = "Despatched";
            //------------------------------------------Report File xl processing   -------------------------------------

            dtsAccount = objServiceHandler.ExecuteQuery(strSQL);

            strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h3 align=center>List of Despatched Transaction</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:18px;font-weight:bold;'><h4 align=center>Print Date (" + String.Format("{0:dd-MMM-yyyy}", DateTime.Now) + ")</h2></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
            strHTML = strHTML + "<tr><td COLSPAN=10 align=center style='border-right:none;font-size:12px;font-weight:bold;'><h3 align=center>Date Range Between " + String.Format("{0:dd-MMM-yyyy}", dptFromDate.DateString) + " and " + String.Format("{0:dd-MMM-yyyy}", dtpToDate.DateString) + "</h3></td></tr>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "</table>";
            strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
            strHTML = strHTML + "<tr>";

            strHTML = strHTML + "<td valign='middle' >Sl</td>";
            strHTML = strHTML + "<td valign='middle' >DPS ID</td>";
            strHTML = strHTML + "<td valign='middle' >Request ID</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Date</td>";
            strHTML = strHTML + "<td valign='middle' >Transaction Amount</td>";
            strHTML = strHTML + "<td valign='middle' >MSS Account</td>";
            strHTML = strHTML + "<td valign='middle' >Upload Status</td>";
            strHTML = strHTML + "<td valign='middle' >DPS Owner</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Request Status</td>";
            strHTML = strHTML + "<td valign='middle' >ISO Request Despatch</td>";
            strHTML = strHTML + "</tr>";

            if (dtsAccount.Tables[0].Rows.Count > 0)
            {
                int SerialNo = 1;
                foreach (DataRow prow in dtsAccount.Tables[0].Rows)
                {
                    strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_DPS_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["REQUEST_ID"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_DATE"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_TRAN_AMT"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_REF_CODE"].ToString() + "</td>";
                    strHTML = strHTML + " <td > '" + prow["UPLOAD_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["DPS_OWNER"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ISO_REQ_STATUS"].ToString() + " </td>";
                    strHTML = strHTML + " <td > '" + prow["CAS_ISO_REQ_DESPATCH"].ToString() + " </td>";
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
            strHTML = strHTML + " </tr>";
            strHTML = strHTML + " </table>";

            SaveAuditInfo("Preview", "Despatched_file_generate");
            clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }


    }
}
