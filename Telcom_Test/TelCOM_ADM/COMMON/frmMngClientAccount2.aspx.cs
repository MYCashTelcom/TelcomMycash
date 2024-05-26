using System;
using System.Data;
using System.Data.OracleClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls.TableRow;


public partial class Forms_frmMngClientAccount2 : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    private static string strUserName = string.Empty;
    private static string strPassword = string.Empty;
    private string strConString = MiT_License.clsDBConnectionReadWrite.GetTelConnectionString();
    public static string strSql = "";

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
        if (MultiView1.ActiveViewIndex == 1)
        {
            MultiView1.ActiveViewIndex = 1;
            lblCheck.Text = "";
        }
        else if (MultiView1.ActiveViewIndex == 0)
        {
            lblMsg.Text = "";
            MultiView1.ActiveViewIndex = 0;            
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

    protected void Page_PreRenderComplete(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (MultiView1.ActiveViewIndex == 1)
            {
                DropDownList20_SelectedIndexChanged(new object(), new EventArgs());
            }
        }
    }

    protected void btnAccountList_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnNewAccount_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void gdvSearch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        LoadSearchResult();
    }
    protected void gdvSearch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        LoadSearchResult();
        gdvSearch.SelectedIndex = e.NewEditIndex;
    }
    protected void gdvSearch_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        LoadSearchResult();
        int index = gdvSearch.EditIndex;
        GridViewRow grow = gdvSearch.Rows[index];
        SaveAuditInfo("Update", "ERS Account New");
    }
    protected void SaveAuditInfo(string strOperationType, string strRemarks)
    {
        string IPAddress = Request.ServerVariables["remote_addr"];
        string Technology = Request.Browser.Browser + Request.Browser.Version;
        string IPTechnology = IPAddress + "-" + Technology;
        objSysAdmin.AddAuditLog(Session["UserID"].ToString(), strOperationType, IPTechnology, objSysAdmin.GetCurrentPageName(), strRemarks);
    }

    protected void gdvSearch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        LoadSearchResult();
    }

    public void LoadSearchResult()
    {
        sdsClientAccount2.SelectCommand = strSql;
        sdsClientAccount2.DataBind();
        gdvSearch.DataBind();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        lblMsg.Text = "";
        strSql = "";

        string strSQL1 = "";
        string strSQL2 = "";
        string strSQL3 = "";

        ArrayList strFinal = new ArrayList();
        //string strFinal = "";

        //string strSql = "";

        string strSQL4 = " (A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID) ";

        if (txtName.Text.ToString().Trim() == "" && txtAccCode.Text.ToString().Trim() == "" && txtMSISDN.Text.ToString().Trim() == "")
        {
            lblMsg.Text = "Enter client name or account code or MSISDN.";
            return;
        }

        if (txtName.Text.ToString().Trim() != "")
        {
            strSQL1 = " (C.CLINT_NAME LIKE '" + txtName.Text.ToString().Trim() + "%') ";
        }

        if (txtAccCode.Text.ToString().Trim() != "")
        {
            strSQL2 = " (A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "') ";
        }

        if (txtMSISDN.Text.ToString().Trim() != "")
        {
            strSQL3 = " (A.ACCNT_MSISDN='" + txtMSISDN.Text.ToString().Trim() + "') ";
        }

        //if (txtName.Text.ToString().Trim() == "")
        //{
        //    lblMsg.Text = "Enter client name.";
        //    return;
        //}
        //else if (txtAccCode.Text.ToString().Trim() == "")
        //{
        //    lblMsg.Text = "Enter account code.";
        //    return;
        //}
        strFinal.Add(strSQL4);

        if (txtName.Text.ToString().Trim() != "" && txtAccCode.Text.ToString().Trim() != "" && txtMSISDN.Text.ToString().Trim() != "")
        {
            //strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE ( " + strSQL1 + " AND " + strSQL2 + " AND " + strSQL3 + " AND " + strSQL4 + ")";
            strFinal.Add(strSQL1);
            strFinal.Add(strSQL2);
            strFinal.Add(strSQL3);
        }
        else
        {
            if (txtName.Text.ToString().Trim() != "")
            {
                strFinal.Add(strSQL1);
            }
            if (txtAccCode.Text.ToString().Trim() != "")
            {
                strFinal.Add(strSQL2);
            }
            if (txtMSISDN.Text.ToString().Trim() != "")
            {
                strFinal.Add(strSQL3);
            }
        }

        string makeQuery = "";
        string[] queryArray = (string[])strFinal.ToArray(typeof(string));
        makeQuery = string.Join(" AND ", queryArray);

        //strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE ( " + makeQuery + ")";
        strSql = "SELECT A.CLINT_ID, ACCNT_ID, ACCNT_NO, ACCNT_MSISDN, CLINT_NAME, A.SERVICE_PKG_ID, SERVICE_PKG_NAME, ACCNT_STATE, ACCNT_RANK_ID FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE ( " + makeQuery + ")";

        //string strSql = "SELECT * FROM ACCOUNT_LIST A, CLIENT_LIST C, SERVICE_PACKAGE S WHERE ((A.ACCNT_NO='" + txtAccCode.Text.ToString().Trim() + "' AND C.CLINT_NAME LIKE '" + txtName.Text.ToString().Trim() + "%') AND A.CLINT_ID=C.CLINT_ID AND A.SERVICE_PKG_ID=S.SERVICE_PKG_ID)";

        // Show Result in Grid
        try
        {
            //sdsClientAccount2.SelectCommand = strSql;
            //sdsClientAccount2.DataBind();
            //gdvSearch.DataBind();
            LoadSearchResult();

            if (gdvSearch.Rows.Count > 0)
            {
                gdvSearch.Visible = true;
                lblMsg.Text = "";
            }
            else
            {
                gdvSearch.Visible = false;
                lblMsg.Text = "Sorry, no data found.";
            }
        }
        catch (Exception ex)
        {
            lblMsg.Text = ex.Message.ToString();
        }
    
    }
    protected void DetailsView1_ItemInserting(object sender, DetailsViewInsertEventArgs e)
    {
      //  sdsClientAccount.InsertParameters["ACCNT_MSISDN"].DefaultValue=
        
    }

    public string searchEasyLoadNo(string strEasyNo)
    {
        
        string DepoID = "";
       
        clsAccountHandler objAcc = new clsAccountHandler();
        DataSet ods = new DataSet();
        ods = objAcc.GetDuplecateID(strEasyNo);

        foreach (DataRow row in ods.Tables["ACCOUNT_LIST"].Rows)
        {
            DepoID = row["ACCNT_MSISDN"].ToString();
            if (DepoID == strEasyNo)
            {                
                return lblCheck.Text = "Duplicate EasyLoad Number";
            }

        }      

        return null;
    }
    protected void Btn_save_Click(object sender, EventArgs e)
    {
        if (TextBox3.Text.ToString().Trim() == "")
        {
            lblCheck.Text = "Enter MSISDN";
            return;
        }
        else if (TextBox3.Text.ToString().Length < 14)
        {
            lblCheck.Text = "Invalid MSISDN";
            return;
        }

        lblCheck.Text = "";
        searchEasyLoadNo(TextBox3.Text);

        clsAccountHandler objAcc = new clsAccountHandler();

        if (lblCheck.Text == "")
        {
            lblCheck.Text = objAcc.AddAccount("", DropDownList19.SelectedValue.ToString(), DropDownList20.SelectedValue.ToString(),
                                               TextBox2.Text, TextBox3.Text, DropDownList18.SelectedValue.ToString());

            TextBox2.Text = "";
            TextBox3.Text = "";
        
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        sdsClientList.DataBind();
    }

    //protected void DropDownList19_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    DropDownList20_SelectedIndexChanged(new object(), new EventArgs());
    //}

    protected void DropDownList20_SelectedIndexChanged(object sender, EventArgs e)
    {
        string cId = DropDownList20.SelectedValue.ToString();
        clsAccountHandler objAH = new clsAccountHandler();
        DataSet oDSChannelInfo = new DataSet();
        oDSChannelInfo = objAH.GetChannelInfo(cId);

        if (oDSChannelInfo.Tables[0].Rows.Count > 0)
        {
            //foreach (DataRow prow in oDSChannelInfo.Tables["CHANNEL_INFO"].Rows)
            //{
            //TextBox2.Text = oDSChannelInfo.Tables[0].Rows[0]["CHANNEL_CODE"].ToString();


          //  TextBox2.Text = oDSChannelInfo.Tables[0].Rows[0]["MSISDN"].ToString().Substring(3)+"1";

            //TextBox2.Text = oDSChannelInfo.Tables[0].Rows[0]["MSISDN"].ToString();
            
            TextBox3.Text = oDSChannelInfo.Tables[0].Rows[0]["MSISDN"].ToString();
       


            //}
        }
        else
        {
            TextBox3.Text = "";
        }
    }
}
