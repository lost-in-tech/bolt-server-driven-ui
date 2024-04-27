using System.Runtime.Serialization;

namespace Bookshop.ServerDriveUI.Elements;

public enum UiSpace
{
    [EnumMember(Value = "None")]
    None,
    [EnumMember(Value = "8Xs")]
    EightXs, // 2
    [EnumMember(Value = "7Xs")]
    SevenXs, // 3
    [EnumMember(Value = "6Xs")]
    SixXs, // 4
    [EnumMember(Value = "5Xs")]
    FiveXs, // 5
    [EnumMember(Value = "4Xs")]
    FourXs, // 6
    [EnumMember(Value = "3Xs")]
    ThreeXs, // 8
    [EnumMember(Value = "2Xs")]
    TwoXs, // 10
    [EnumMember(Value = "Xs")]
    Xs, //12
    [EnumMember(Value = "Sm")]
    Sm, // 14
    [EnumMember(Value = "Md")]
    Md, // 16
    [EnumMember(Value = "Lg")]
    Lg, // 18
    [EnumMember(Value = "Xl")]
    Xl, // 20
    [EnumMember(Value = "2Xl")]
    TwoXl, // 22
    [EnumMember(Value = "3Xl")]
    ThreeXl, // 24
    [EnumMember(Value = "4Xl")]
    FourXl, // 26
    [EnumMember(Value = "5Xl")]
    FiveXl, // 28
    [EnumMember(Value = "6Xl")]
    SixXl, // 30
}