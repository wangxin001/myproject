using System;
using Action;

/// <summary>
///IAction 的摘要说明
/// </summary>
public interface IAction
{
    void perform(String param, XmlResult result);
}