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

public partial class System_SYS_Password_Change : System.Web.UI.Page
{
    clsSystemAd objclsSystemAdmin = new clsSystemAd();
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    string strOldPass;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            txtLogInName.Text = Session["UserLoginName"].ToString();
            
            //try
            //{
            //    lblSpMessage.Text = "";
            //    ddlBranch.SelectedValue = Session["Branch_ID"].ToString();
            //    if (Session["Branch_Type"].Equals("A"))
            //    {
            //        ddlBranch.Enabled = true;
            //    }
            //    else
            //    {
            //        ddlBranch.Enabled = false;
            //    }
            //}

            //catch
            //{
            //    Response.Redirect("../frmSeesionExpMesage.aspx");
            //}

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

    public void vChangePass()
    {
        //strOldPass = Session["Password"].ToString();
        string strUserID = Session["UserID"].ToString();


        string strSql = "SELECT GET_CM_USER_PASS('" + strUserID + "','GWTRM21943309') USER_PASS FROM DUAL";
        clsServiceHandler objServiceHandler = new clsServiceHandler();
        DataSet oDs = objServiceHandler.ExecuteQuery(strSql);
        if (oDs.Tables[0].Rows.Count > 0)
        {
            int SerialNo = 1;
            foreach (DataRow prow in oDs.Tables[0].Rows)
            {
                strOldPass = prow["USER_PASS"].ToString();
            }
        }



        string strMessage = "";
        try
        {
            if (txtNPassword.Text.ToString().Length > 0 || txtNPassword.Text.ToString() == " " || txtLogInName.Text.ToString().Trim().Length > 0)
            {
                if (txtOldPass.Text.ToString() == strOldPass)
                {
                    if (txtNPassword.Text.ToString() == txtRepass.Text.ToString())
                    {
                        strMessage = objclsSystemAdmin.sChangePassword(strUserID,
                                                                       txtNPassword.Text.ToString(),
                                                                       txtLogInName.Text.ToString());
                        lblSpMessage.Text = strMessage;
                    }
                    else
                    {
                        lblSpMessage.Text = "Password Mismatched";
                        txtOldPass.Text = "";
                    }
                }
                else
                {
                    lblSpMessage.Text = "Password Mismatched";
                    txtOldPass.Text = "";
                }
            }
            else
            {
                lblSpMessage.Text = "No Password/ Login Name Found";
            }
        }
        catch (Exception ex)
        {
            //HttpContext.Current.Response.Write(ex);
            lblSpMessage.Text = ex.ToString();
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        vChangePass();
        txtNPassword.Text = "";
        txtRepass.Text = "";
        Session["UserLoginName"] = txtLogInName.Text.ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtNPassword.Text = "";
        txtRepass.Text = "";
        txtOldPass.Text = "";
    }
}
