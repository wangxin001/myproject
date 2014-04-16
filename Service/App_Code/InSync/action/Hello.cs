
namespace Action
{

    /// <summary>
    ///Hello 的摘要说明
    /// </summary>
    public class Hello : IAction
    {
        public void perform(string param, XmlResult result)
        {
            result.AddHeader("total", "1");
        }
    }
}