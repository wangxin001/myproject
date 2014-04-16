using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace model
{
    public enum UserRole
    {
        系统管理员 = 1,
        查看所有菜价用户 = 2,
        数据上报用户 = 3,
        查询菜价接口用户 = 4
    }

    public enum Status
    {
        /// <summary>
        /// 正常 =1
        /// </summary>
        [Description("正常")]
        Normal = 1,
        /// <summary>
        /// 禁用 =2
        /// </summary>
        [Description("禁用")]
        Forbidden = 0
    }

    public enum BoardType
    {
        /// <summary>
        /// 普通新闻 =1
        /// </summary>
        InfoPub = 1,
        /// <summary>
        /// 图片新闻 =2
        /// </summary>
        Picture = 2,
        /// <summary>
        /// 问答 =3
        /// </summary>
        QAndA = 3,
        /// <summary>
        /// 文件下载类 =4
        /// </summary>
        File = 4
    }

    public enum PriceType
    {
        /// <summary>
        /// 蔬菜价格 =1
        /// </summary>
        [Description("今日蔬菜价格")]
        Price_VegetablePrice = 1,
        /// <summary>
        /// 大宗货物价格 =2
        /// </summary>
        [Description("大宗货品")]
        Price_CargoPrice = 2,
        /// <summary>
        /// 蔬菜价格指数 =3
        /// </summary>
        [Description("蔬菜价格指数")]
        Index_VPI = 3,
        /// <summary>
        /// 消费价格指数 =4
        /// </summary>
        [Description("消费价格指数")]
        Index_CPI = 4,
        /// <summary>
        /// 原油价格指数 =5
        /// </summary>
        [Description("原油价格指数")]
        Index_YuanYou = 5,
        /// <summary>
        /// 大豆价格指数 =6
        /// </summary>
        [Description("大豆价格指数")]
        Index_DaDou = 6,
        /// <summary>
        /// 玉米价格指数 =7
        /// </summary>
        [Description("玉米价格指数")]
        Index_YuMi = 7,
        /// <summary>
        /// 小麦价格指数 =8
        /// </summary>
        [Description("小麦价格指数")]
        Index_XiaoMai = 8,

        /// <summary>
        /// 菜价走势
        /// </summary>
        [Description("菜价走势")]
        Chart_CaiJia = 9,
        /// <summary>
        /// 富强粉走势
        /// </summary>
        [Description("富强粉走势")]
        Chart_FuQiangFen = 10,
        /// <summary>
        /// 粳米走势
        /// </summary>
        [Description("粳米价格")]
        Chart_JingMi = 11,
        /// <summary>
        /// 调和油走势
        /// </summary>
        [Description("调和油走势")]
        Chart_TiaoHeYou = 12,
        /// <summary>
        /// 猪肉走势
        /// </summary>
        [Description("猪肉走势")]
        Chart_ZhuRou = 13,
        /// <summary>
        /// 鸡蛋走势
        /// </summary>
        [Description("鸡蛋走势")]
        Chart_JiDan = 14,

        /// <summary>
        /// 每日粮油
        /// </summary>
        [Description("每日粮油")]
        Price_LiangYou = 15,
        /// <summary>
        /// 肉蛋水产
        /// </summary>
        [Description("肉蛋水产")]
        Price_RouDan = 16,
        /// <summary>
        /// 上报菜价
        /// </summary>
        [Description("上报菜价")]
        StorePrice = 17,
        /// <summary>
        /// 同步保存内容
        /// </summary>
        [Description("长久保存价格内容")]
        PersistentPrice = 18
    }

    /// <summary>
    /// 投票调查状态
    /// </summary>
    public enum VoteStatus
    {
        /// <summary>
        /// 投票尚未开始
        /// </summary>
        NotStart = 0,
        /// <summary>
        /// 投票进行中
        /// </summary>
        Voting = 1,
        /// <summary>
        /// 投票已结束
        /// </summary>
        VoteEnd = 2
    }

    /// <summary>
    /// 调查问卷问题类型
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// 单选题
        /// </summary>
        SingleSelect = 1,
        /// <summary>
        /// 多选题
        /// </summary>
        MultiSelect = 2
    }
}

namespace LogUtil
{
    public enum Level
    {
        Debug = 1,
        Info = 2,
        Warn = 3,
        Error = 4,
        Fatal = 5
    }
}