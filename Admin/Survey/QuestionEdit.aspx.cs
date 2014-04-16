using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

public partial class Survey_QuestionEdit : System.Web.UI.Page
{
    String strQuestionID = String.Empty;
    String strVoteID = String.Empty;

    model.VoteQuestion question = null;
    List<model.VoteOption> options = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        strQuestionID = Request.QueryString["QuestionID"];
        strVoteID = Request.QueryString["VoteID"];

        if (!IsPostBack)
        {
            if (!String.IsNullOrEmpty(strQuestionID))
            {
                LoadQuestionInfo();
            }

            LoadControls();
        }
    }


    /// <summary>
    /// 根据配置文件中 SurveyOptionsMaxCount 的配置生成下拉框中可选的选项数量
    /// </summary>
    protected void LoadControls()
    {
        // 单选多选，题型选择先加上
        this.ddlQuestionType.Items.Add(new ListItem("单选题", ((int)model.QuestionType.SingleSelect).ToString()));
        this.ddlQuestionType.Items.Add(new ListItem("多选题", ((int)model.QuestionType.MultiSelect).ToString()));


        // 处理选项数量选择框
        String strCount = System.Configuration.ConfigurationManager.AppSettings["SurveyOptionsMaxCount"];

        // 最多可选数量：10个
        int maxCount = 10;
        if (!int.TryParse(strCount, out maxCount))
        {
            maxCount = 10;
        }

        // 当前问题有几个选项
        int oCount = 4;
        oCount = (question == null ? 4 : question.OptionsCount);

        // 根据数量动态创建下拉选项数量
        for (int i = 0; i < maxCount; i++)
        {
            this.ddlOptionCount.Items.Add(new ListItem((i + 1).ToString() + "个", (i + 1).ToString()));
        }

        this.ddlOptionCount.SelectedValue = oCount.ToString();

        // 动态创建当前页面的选项内容

        for (int i = 0; i < maxCount; i++)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");

            div.Attributes["id"] = "divOption" + i.ToString();
            div.Attributes["class"] = "OptionList";

            System.Text.StringBuilder strInput = new System.Text.StringBuilder();

            strInput.Append(String.Format("<input type='text' id='option{0}' name='option{0}' class='TextBorder Width-180' ", i));

            // 如果当前问题不为空，当前索引小于选项个数，并且存在指定索引的选项
            if ((question != null) && (i < oCount) && ((i + 1) < options.Count))
            {
                strInput.Append("value='" + options[i].OptionContent + "'>");
            }
            else
            {
                strInput.Append("value=''>");
            }

            div.InnerHtml = strInput.ToString();

            this.Panel1.Controls.Add(div);
        }
    }

    /// <summary>
    /// 加载所选的问题内容
    /// </summary>
    protected void LoadQuestionInfo()
    {
        question = OR.Control.DAL.GetModelByKey<model.VoteQuestion>(Convert.ToInt32(strQuestionID));

        if (question != null)
        {
            options = OR.Control.DAL.GetModelList<model.VoteOption>(" QuestionID = " + strQuestionID + " ORDER By OptionOrder ");

            this.txtContent.Text = question.QuestionContent;
            this.ddlQuestionType.SelectedValue = question.QuestionType.ToString();
            this.ddlOptionCount.SelectedValue = options.Count.ToString();
            this.txtOrder.Text = question.DisplayOrder.ToString();
        }
    }


    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //  这里是对现有试题内容的更新处理。试题和选项可以分开搞，通过试题号关联起来即可
        if (!String.IsNullOrEmpty(strQuestionID))
        {
            // 更新内容
            question = OR.Control.DAL.GetModelByKey<model.VoteQuestion>(Convert.ToInt32(strQuestionID));
            question.QuestionContent = this.txtContent.Text;
            question.DisplayOrder = Convert.ToInt32(this.txtOrder.Text);
            question.OptionsCount = Convert.ToInt32(this.ddlOptionCount.SelectedValue);
            question.QuestionType = Convert.ToInt32(this.ddlQuestionType.SelectedValue);
            question.Status = (int)model.Status.Normal;

            OR.Control.DAL.Update<model.VoteQuestion>(question);

            Log.info("更新问题内容：" + question.ToString(true));

        }
        else
        {
            question = new model.VoteQuestion();
            question.QuestionContent = this.txtContent.Text;
            question.DisplayOrder = Convert.ToInt32(this.txtOrder.Text);
            question.OptionsCount = Convert.ToInt32(this.ddlOptionCount.SelectedValue);
            question.QuestionType = Convert.ToInt32(this.ddlQuestionType.SelectedValue);
            question.Status = (int)model.Status.Normal;
            question.Created = DateTime.Now;
            question.UserGUID = Util.GetLoginUserInfo().UserGUID;
            question.UserName = Util.GetLoginUserInfo().UserName;
            question.VoteID = Convert.ToInt32(strVoteID);

            question = OR.Control.DAL.Add<model.VoteQuestion>(question, true);

            Log.info("添加新问题：" + question.ToString(true));
        }

        // 开始处理选项
        // 选项好弄，这个就直接判断数量就行
        options = OR.Control.DAL.GetModelList<model.VoteOption>(" QuestionID = " + question.QuestionID.ToString() + " ORDER By OptionOrder ");

        int oCount = Convert.ToInt32(this.ddlOptionCount.SelectedValue);

        // 逐个的，先将现在的数据记录下来，再考虑删除的问题
        // 要记录的数据，从0到头，这些要记录，有就更新，不够就添加
        for (int i = 0; i < oCount; i++)
        {
            // 当前的是原来就有的东西，做更新
            if (i < options.Count)
            {
                model.VoteOption option = options[i];
                option.OptionContent = Request.Form["Option" + i.ToString()];
                option.OptionOrder = i;
                OR.Control.DAL.Update<model.VoteOption>(option);

                Log.info("更新问题选项：" + option.ToString(true));
            }
            else
            {
                model.VoteOption option = new model.VoteOption();
                option.Created = DateTime.Now;
                option.OptionContent = Request.Form["Option" + i.ToString()];
                option.OptionOrder = i;
                option.QuestionID = question.QuestionID;
                option.VoteID = question.VoteID;

                option = OR.Control.DAL.Add<model.VoteOption>(option, true);

                Log.info("添加问题选项：" + option.ToString(true));
            }
        }

        // 上面的完了，这里该做多余选项的删除了
        for (int i = oCount; i < options.Count; i++)
        {
            model.VoteOption option = options[i];
            OR.Control.DAL.Delete<model.VoteOption>(option.OptionID);

            Log.info("删除问题选项：" + option.ToString(true));
        }

        ClientScript.RegisterStartupScript(typeof(Page), "script", "<script>parent.CloseFrame();parent.location.href=parent.location.href;</script>");
    }
}