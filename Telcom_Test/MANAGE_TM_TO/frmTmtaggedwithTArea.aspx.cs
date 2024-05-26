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

public partial class MANAGE_TM_TO_frmTmtaggedwithTArea : System.Web.UI.Page
{
    clsSystemAdmin objSysAdmin = new clsSystemAdmin();
    clsServiceHandler objServiceHandler = new clsServiceHandler();
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

            LoadTmList();
            LoadArea();
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

    private void LoadArea()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT AREA_ID, AREA_NAME FROM MANAGE_AREA ORDER BY AREA_NAME ASC";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            drpArea.DataSource = oDataSet;
            drpArea.DataBind();
            drpArea.Items.Insert(0, new ListItem("Select Area"));
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }

    private void LoadTmList()
    {
        try
        {
            string strSql = "";
            strSql = " SELECT DISTINCT AL.ACCNT_ID, AL.ACCNT_NO||'('||CL.CLINT_NAME || ', ' || CL.CLINT_ADDRESS1 ||')' UPPER_HIERARCHY_INFO "
                     + " FROM ACCOUNT_LIST AL, MANAGE_TERRITORY_RANK MTR, CLIENT_LIST CL WHERE "
                     + " AL.TERRITORY_RANK_ID = MTR.TERRITORY_RANK_ID AND MTR.TERRITORY_RANK_ID = '150121000000000001' "
                     + " AND AL.ACCNT_RANK_ID = '120519000000000006' AND AL.CLINT_ID = CL.CLINT_ID ";
            DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
            drpTmList.DataSource = oDataSet;
            drpTmList.DataBind();
            drpTmList.Items.Insert(0, new ListItem("Select TM"));
        }
        catch (Exception exception )
        {
            exception.Message.ToString();
        }
    }
    protected void btnAreaInfo_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpArea.SelectedIndex == 0)
            {
                lblMessage.Text = "Select Area";
                return;
            }
            else
            {
                dtvAreaInfo.Visible = true;
                string strSql = "";
                strSql = " SELECT MA.AREA_NAME, MR.REGION_NAME, AL.ACCNT_NO||', '||CL.CLINT_NAME||', '||CL.CLINT_ADDRESS1 TMINFO "
                         + " FROM MANAGE_AREA MA, MANAGE_REGION MR, MANAGE_TERRITORY_AREA MTA, ACCOUNT_LIST AL, CLIENT_LIST CL "
                         + " WHERE MA.AREA_ID = '" + drpArea.SelectedValue + "' AND MA.REGION_ID = MR.REGION_ID(+) "
                         + " AND MA.AREA_ID = MTA.AREA_ID(+) AND MTA.ACCNT_ID = AL.ACCNT_ID(+) AND AL.CLINT_ID = CL.CLINT_ID(+) ";

                DataSet oDataSet = objServiceHandler.ExecuteQuery(strSql);
                dtvAreaInfo.DataSource = oDataSet;
                dtvAreaInfo.DataBind();
            }


        }
        catch (Exception exception )
        {
            exception.Message.ToString();
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (drpArea.SelectedIndex == 0 || drpTmList.SelectedIndex == 0)
            {
                lblMessage.Text = "Select Area or TM";
                return;
            }
            else
            {
                string strAreaIfExist = objServiceHandler.TerritoryAreaIdIfExist(drpArea.SelectedValue);
                if (strAreaIfExist != "")
                {
                    // update
                    string strUpdateSuccMsg = objServiceHandler.UpdateTMTaggedWithArea(drpArea.SelectedValue,
                        drpTmList.SelectedValue);
                    if (strUpdateSuccMsg == "Successfull.")
                    {
                        lblMessage.Text = "Tagged Successfully";
                    }
                    else
                    {
                        lblMessage.Text = strUpdateSuccMsg;
                    }
                }
                else
                {
                    // add
                    string strAddSuccMsg = objServiceHandler.AddToManageTerritoryArea(drpTmList.SelectedValue,
                        drpArea.SelectedValue);
                    if (strAddSuccMsg == "Successfull.")
                    {
                        lblMessage.Text = "Tagged Successfully";
                    }
                    else
                    {
                        lblMessage.Text = strAddSuccMsg;
                    }

                }
            }
        }
        catch (Exception exception)
        {
            exception.Message.ToString();
        }
    }
}
