using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class RESET_PASSWORD : System.Web.UI.Page
{

    clsSystemAdmin clssystem = new clsSystemAdmin();
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmLogin.aspx", false);
    }
    protected void btnSave_Click1(object sender, EventArgs e)
    {
        if (txtLoginName.Text.Trim().Equals(""))
        {
            lblSpMessage.Text = "Please input username";
            return;
        }
        else if (txtOldPassword.Text.Trim().Equals(""))
        {
            lblSpMessage.Text = "Please input old password";
            return;
        }
        else if (txtNewpass.Text.Trim().Equals(""))
        {
            lblSpMessage.Text = "Please input new password";
            return;
        }
        else if (!txtNewpass.Text.Trim().Equals(txtConfirmPassword.Text.Trim()))
        {
            lblSpMessage.Text = "New password and confirm password are not matched.";
            return;
        }


        String name = txtLoginName.Text;
        String oldpassword = txtOldPassword.Text;
        String newpassword = txtNewpass.Text;
        DataBaseClassOracle db = new DataBaseClassOracle();
        String sessionId = clssystem.GetSessionID(name);
        DataSet ds = clssystem.DecryptPassword(sessionId, name, clssystem.getUserId(name));
        String qry = "select * from CM_SYSTEM_USERS where SYS_USR_LOGIN_NAME='" + name + "'";
        DataTable dt=db.ConnectDataBaseReturnDT(qry);
        try
        {
            if (ds.Tables[0].Rows[0]["EXIS_PASS"].ToString() == oldpassword)
            {
                if (dt.Rows[0]["LOCKED_STATUS"].ToString() == "UL")
                {
                    string expireDate = "SELECT DISTINCT PP_PASS_EXPIRE_AFTER_DAYS FROM CM_SYSTEM_PASSWORD_POLICY";
                    DataTable dtExpireDate = db.ConnectDataBaseReturnDT(expireDate);
                    int days = Convert.ToInt32(dtExpireDate.Rows[0]["PP_PASS_EXPIRE_AFTER_DAYS"].ToString());

                    qry = "update CM_SYSTEM_USERS set SYS_USR_PASS='" + newpassword + "', SYS_USR_PASS_PREVIOUS='" + oldpassword + "', PASSWORD_EXPIRED_DATE = SYSDATE + " + days + " where SYS_USR_LOGIN_NAME='" + name + "'";
                    db.ConnectDataBaseToInsert(qry);
                }
                else
                {
                    lblSpMessage.Text = "You are currently locked Please contact your Adminstrator";
                    return;
                }
            }
            else
            {
                lblSpMessage.Text = "OLD PASSWORD DIDNT MATCH";
                return;
            }
        }
        catch(Exception ex)
        {
            lblSpMessage.Text = ex.Message.ToString();
            return;
        }
        lblSpMessage.Text = "Successfully Changed";
        txtLoginName.Text = "";
        btnLogin.Visible = true;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmLogin.aspx", false);
    }
}
