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

public partial class COMMON_frmNLIDeviceInfoActiveDeactive : System.Web.UI.Page
{
    clsServiceHandler objServiceHandler = new clsServiceHandler();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                LoadRequestList();
                                
                strUserName = Session["UserLoginName"].ToString();
                strPassword = Session["Password"].ToString();
            }
            catch (Exception ex)
            {
                Session.Clear();
                Response.Redirect("../frmSeesionExpMesage.aspx");
                ex.Message.ToString();
            }
        }
        gdvRequest.RowUpdating += new GridViewUpdateEventHandler(gdvRequest_RowUpdating);
        lblMessage.Text = "";
    }

    public void LoadRequestList()
    {
        string strSQL = "";

        //strSQL = "SELECT  ACCNT_POS_ID,TERMINAL_NAME,TERMINAL_SERIAL_NO,ACTIVE_STATUS,CHANNEL_TYPE FROM ACCOUNT_POS_LIST WHERE ACTIVE_STATUS = '"+ddlStatus.SelectedValue.ToString()+"' ORDER BY  ACCNT_POS_ID DESC ";

        if (ddlStatus.SelectedValue.ToString()=="I")
        {
            strSQL = "SELECT ACCNT_POS_ID,TERMINAL_NAME,TERMINAL_SERIAL_NO,ACTIVE_STATUS,CHANNEL_TYPE, ORG_NAME, OFFICE_NAME, ZONE_NAME, CASHIER_NAME, DESIGNATION, NLIC_CASHIER_ID_NO, MOBILE_NO, IMEI_NO_1, IMEI_NO_2, NLI_INFO_ENTRY_DATE FROM ACCOUNT_POS_LIST WHERE ORG_NAME = 'NLIC' AND ACTIVE_STATUS = '" + ddlStatus.SelectedValue.ToString() + "'  ";
        }
        else if (ddlStatus.SelectedValue.ToString() == "A")
        {
            strSQL = "SELECT ACCNT_POS_ID,TERMINAL_NAME,TERMINAL_SERIAL_NO,ACTIVE_STATUS,CHANNEL_TYPE, ORG_NAME, OFFICE_NAME, ZONE_NAME, CASHIER_NAME, DESIGNATION, NLIC_CASHIER_ID_NO, MOBILE_NO, IMEI_NO_1, IMEI_NO_2, NLI_INFO_ENTRY_DATE FROM ACCOUNT_POS_LIST WHERE ORG_NAME = 'NLIC' AND ACTIVE_STATUS = 'A'";
        }
        else
        {
            strSQL = "SELECT ACCNT_POS_ID,TERMINAL_NAME,TERMINAL_SERIAL_NO,ACTIVE_STATUS,CHANNEL_TYPE, ORG_NAME, OFFICE_NAME, ZONE_NAME, CASHIER_NAME, DESIGNATION, NLIC_CASHIER_ID_NO, MOBILE_NO, IMEI_NO_1, IMEI_NO_2, NLI_INFO_ENTRY_DATE FROM ACCOUNT_POS_LIST WHERE ORG_NAME = 'NLIC'";
        }
        if(txtsearch.Text.Trim().ToString()!="")
        {
            strSQL = strSQL + " AND TERMINAL_SERIAL_NO = '" + txtsearch.Text.ToString() + "'";
        }
        strSQL = strSQL + " ORDER BY ACCNT_POS_ID DESC";
       
        try
        {
            DataSet dtsAccount = new DataSet();
            dtsAccount = objServiceHandler.ExecuteQuery(strSQL);

            gdvRequest.DataSource = dtsAccount;
            gdvRequest.DataBind();
            
        }
        catch (Exception ex)
        {
            ex.Message.ToString();
        }
    }


    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadRequestList();
    }



    protected void gdvRequest_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gdvRequest.EditIndex = -1;
        LoadRequestList();
    }
    protected void gdvRequest_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridViewRow currentRow = this.gdvRequest.Rows[e.NewEditIndex];
        Label lblAccountPosId = currentRow.FindControl("Label1") as Label;

        string accountPosId = lblAccountPosId.Text;
        hfAccountPosId.Value = accountPosId;

        string strSQL = " SELECT TERMINAL_NAME, TERMINAL_SERIAL_NO, ACTIVE_STATUS, CHANNEL_TYPE,"
                        + " OFFICE_NAME, ZONE_NAME, CASHIER_NAME, DESIGNATION, NLIC_CASHIER_ID_NO,"
                        + " MOBILE_NO, IMEI_NO_1, IMEI_NO_2 FROM ACCOUNT_POS_LIST WHERE"
                        + " ACCNT_POS_ID = '" + accountPosId + "' ";

        DataSet oDs = new DataSet();

        oDs = objServiceHandler.ExecuteQuery(strSQL);

        txtTerminalName.Text = oDs.Tables[0].Rows[0]["TERMINAL_NAME"].ToString();
        txtTerminalSerial.Text = oDs.Tables[0].Rows[0]["TERMINAL_SERIAL_NO"].ToString();
        ddlActiveStatus.SelectedValue = oDs.Tables[0].Rows[0]["ACTIVE_STATUS"].ToString();
        txtChannelType.Text = oDs.Tables[0].Rows[0]["CHANNEL_TYPE"].ToString();
        txtOfficeName.Text = oDs.Tables[0].Rows[0]["OFFICE_NAME"].ToString();
        txtZoneName.Text = oDs.Tables[0].Rows[0]["ZONE_NAME"].ToString();
        txtCashierName.Text = oDs.Tables[0].Rows[0]["CASHIER_NAME"].ToString();
        txtDesignation.Text = oDs.Tables[0].Rows[0]["DESIGNATION"].ToString();
        txtNLICCashierID.Text = oDs.Tables[0].Rows[0]["NLIC_CASHIER_ID_NO"].ToString();
        txtMobileNo.Text = oDs.Tables[0].Rows[0]["MOBILE_NO"].ToString();
        txtIMEI1.Text = oDs.Tables[0].Rows[0]["IMEI_NO_1"].ToString();
        txtIMEI2.Text = oDs.Tables[0].Rows[0]["IMEI_NO_2"].ToString();
    }
    protected void gdvRequest_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            string accountPosId = gdvRequest.DataKeys[e.RowIndex][0].ToString();
            DropDownList status = (DropDownList)gdvRequest.Rows[e.RowIndex].FindControl("ddlStatusE");
            TextBox officeName = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtOfficeName");
            TextBox zoneName = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtZoneName");
            TextBox cashierName = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtCashierName");
            TextBox designation = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtDesignation");
            TextBox nLicCIdNo = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtNlicCIdNo");
            TextBox mobileNo = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtMobileNo");
            TextBox iMei1 = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtImei1");
            TextBox iMei2 = (TextBox)gdvRequest.Rows[e.RowIndex].FindControl("txtImei2");
            
            string strStatus = status.Text;
            string strOfficeName = officeName.Text;
            string strZoneName = zoneName.Text;
            string strCashierName = cashierName.Text;
            string strDesignation = designation.Text;
            string strNlicCIdNo = nLicCIdNo.Text;
            string strMobileNo = mobileNo.Text;
            string strImei1 = iMei1.Text;
            string strImei2 = iMei2.Text;


            string strMsg = objServiceHandler.UpdateNliDeviceStatus(accountPosId, strStatus, strOfficeName, strZoneName, strCashierName, strDesignation, strNlicCIdNo, strMobileNo, strImei1, strImei2);

            if (strMsg == "Successfull.")
            {
                lblMessage.Text = "Status updated successfully";
                //ddlStatus.SelectedValue = strNewValue;
            }
            gdvRequest.EditIndex = -1;
            LoadRequestList();

            string strUpdate = "Update: " + strStatus + ", AccountPosId:" + accountPosId;
            SaveAuditInfo("Update", strUpdate);
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
    protected void gdvRequest_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        gdvRequest.EditIndex = -1;
        LoadRequestList();
    }

    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void btnSerach_Click(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void BtnRefresh_Click(object sender, EventArgs e)
    {
        LoadRequestList();
    }
    protected void btnAddNewDevice_Click(object sender, EventArgs e)
    {
        try
        {
            string accountPosId = hfAccountPosId.Value;
            string strStatus = ddlActiveStatus.SelectedValue;
            string strOfficeName = txtOfficeName.Text;
            string strZoneName = txtZoneName.Text;
            string strCashierName = txtCashierName.Text;
            string strDesignation = txtDesignation.Text;
            string strNlicCIdNo = txtNLICCashierID.Text;
            string strMobileNo = txtMobileNo.Text;
            string strImei1 = txtIMEI1.Text;
            string strImei2 = txtIMEI2.Text;
            
            string strMsg = objServiceHandler.UpdateNliDeviceStatus(accountPosId, strStatus, strOfficeName, strZoneName, strCashierName, strDesignation, strNlicCIdNo, strMobileNo, strImei1, strImei2);

            if (strMsg == "Successfull.")
            {
                lblMessage.Text = "Status updated successfully";
                ClearText();
                //ddlStatus.SelectedValue = strNewValue;
            }
            gdvRequest.EditIndex = -1;
            LoadRequestList();

            string strUpdate = "Update: " + strStatus + ", AccountPosId:" + accountPosId;
            SaveAuditInfo("Update", strUpdate);
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void ClearText()
    {
        hfAccountPosId.Value = "";
        txtTerminalName.Text = "";
        txtTerminalSerial.Text = "";
        ddlActiveStatus.SelectedIndex = 0;
        txtChannelType.Text = "";
        txtOfficeName.Text = "";
        txtZoneName.Text = "";
        txtCashierName.Text = "";
        txtDesignation.Text = "";
        txtNLICCashierID.Text = "";
        txtMobileNo.Text = "";
        txtIMEI1.Text = "";
        txtIMEI2.Text = "";
    }
    protected void gdvRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gdvRequest.PageIndex = e.NewPageIndex;
        LoadRequestList();
    }
    protected void btnExportReport_Click(object sender, EventArgs e)
    {
        string strSQL = "";

        if (ddlStatus.SelectedValue.ToString() == "I")
        {
            strSQL = "SELECT ACCNT_POS_ID,TERMINAL_NAME,TERMINAL_SERIAL_NO,DECODE(ACTIVE_STATUS, 'A', 'Active', 'I', 'InActive') ACTIVE_STATUS,CHANNEL_TYPE, ORG_NAME, OFFICE_NAME, ZONE_NAME, CASHIER_NAME, DESIGNATION, NLIC_CASHIER_ID_NO, MOBILE_NO, IMEI_NO_1, IMEI_NO_2, NLI_INFO_ENTRY_DATE FROM ACCOUNT_POS_LIST WHERE ORG_NAME = 'NLIC' AND ACTIVE_STATUS = '" + ddlStatus.SelectedValue.ToString() + "'  ";
        }
        else if (ddlStatus.SelectedValue.ToString() == "A")
        {
            strSQL = "SELECT ACCNT_POS_ID,TERMINAL_NAME,TERMINAL_SERIAL_NO,DECODE(ACTIVE_STATUS, 'A', 'Active', 'I', 'InActive') ACTIVE_STATUS,CHANNEL_TYPE, ORG_NAME, OFFICE_NAME, ZONE_NAME, CASHIER_NAME, DESIGNATION, NLIC_CASHIER_ID_NO, MOBILE_NO, IMEI_NO_1, IMEI_NO_2, NLI_INFO_ENTRY_DATE FROM ACCOUNT_POS_LIST WHERE ORG_NAME = 'NLIC' AND ACTIVE_STATUS = 'A'";
        }
        else 
        {
            strSQL = "SELECT ACCNT_POS_ID,TERMINAL_NAME,TERMINAL_SERIAL_NO,DECODE(ACTIVE_STATUS, 'A', 'Active', 'I', 'InActive') ACTIVE_STATUS,CHANNEL_TYPE, ORG_NAME, OFFICE_NAME, ZONE_NAME, CASHIER_NAME, DESIGNATION, NLIC_CASHIER_ID_NO, MOBILE_NO, IMEI_NO_1, IMEI_NO_2, NLI_INFO_ENTRY_DATE FROM ACCOUNT_POS_LIST WHERE ORG_NAME = 'NLIC'";
        }
        if (txtsearch.Text.Trim().ToString() != "")
        {
            strSQL = strSQL + " AND TERMINAL_SERIAL_NO = '" + txtsearch.Text.ToString() + "'";
        }
        strSQL = strSQL + " ORDER BY ACCNT_POS_ID DESC";

        string strHTML = "", fileName = "";
        lblMessage.Text = "";

        DataSet dtsAccount = new DataSet();
        fileName = "Device_ActiveDe_Rpt";
        //------------------------------------------Report File xl processing   -------------------------------------

        dtsAccount = objServiceHandler.ExecuteQuery(strSQL);

        strHTML = strHTML + "<table border=\"0\" width=\"80%\">";
        strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h3 align=center>National Life Insurance</h3></td></tr>";
        strHTML = strHTML + "</table>";
        
        strHTML = strHTML + "<table border=\"0\" width=\"100%\">";
        strHTML = strHTML + "<tr><td COLSPAN=12 align=center style='border-right:none;font-size:14px;font-weight:bold;'><h4 align=center>Device Active DeActive Informaiton</h2></td></tr>";
        strHTML = strHTML + "</table>";

        strHTML = strHTML + "<table border=\"1\" width=\"100%\">";
        strHTML = strHTML + "<tr>";

        strHTML = strHTML + "<td valign='middle' >Sl</td>";
        strHTML = strHTML + "<td valign='middle' >Terminal Name</td>";
        strHTML = strHTML + "<td valign='middle' >Terminal Serial No.</td>";
        strHTML = strHTML + "<td valign='middle' >Active Status</td>";
        strHTML = strHTML + "<td valign='middle' >Channel Type</td>";
        strHTML = strHTML + "<td valign='middle' >Office Name</td>";
        strHTML = strHTML + "<td valign='middle' >Zone Name</td>";
        strHTML = strHTML + "<td valign='middle' >Cashier Name</td>";
        strHTML = strHTML + "<td valign='middle' >Designation</td>";
        strHTML = strHTML + "<td valign='middle' >NLIC Cashier ID No.</td>";
        strHTML = strHTML + "<td valign='middle' >Mobile No.</td>";
        strHTML = strHTML + "<td valign='middle' >IMEI 1</td>";
        strHTML = strHTML + "<td valign='middle' >IMEI 2</td>";
        strHTML = strHTML + "<td valign='middle' >Info Entry Date</td>";
        
        strHTML = strHTML + "</tr>";

        if (dtsAccount.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;

            foreach (DataRow prow in dtsAccount.Tables[0].Rows)
            {
                string date = prow["NLI_INFO_ENTRY_DATE"].ToString() != "" ? Convert.ToDateTime(prow["NLI_INFO_ENTRY_DATE"]).ToString("dd-MMM-yyyy") : prow["NLI_INFO_ENTRY_DATE"].ToString();
                
                strHTML = strHTML + " <tr><td >" + SerialNo.ToString() + "</td>";
                strHTML = strHTML + " <td > '" + prow["TERMINAL_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["TERMINAL_SERIAL_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["ACTIVE_STATUS"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["CHANNEL_TYPE"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["OFFICE_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["ZONE_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["CASHIER_NAME"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["DESIGNATION"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["NLIC_CASHIER_ID_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["MOBILE_NO"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["IMEI_NO_1"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" + prow["IMEI_NO_2"].ToString() + " </td>";
                strHTML = strHTML + " <td > '" +  date + " </td>";
                
                strHTML = strHTML + " </tr> ";
                SerialNo = SerialNo + 1;
            }
        }
        //strHTML = strHTML + "<tr>";
        //strHTML = strHTML + " <td > " + "" + " </td>";
        //strHTML = strHTML + " <td > " + "" + " </td>";
        //strHTML = strHTML + " <td > " + "" + " </td>";
        //strHTML = strHTML + " </tr>";
        strHTML = strHTML + " </table>";
        SaveAuditInfo("Preview", "Manage_Service_Fee_Rpt");
        clsGridExport.ExportToMSExcel(fileName, "msexcel", strHTML, "landscape");
        lblMessage.ForeColor = System.Drawing.Color.White;
        lblMessage.Text = "Report Generated Successfully...";
    }
}
