using System.Runtime.Serialization;

namespace BigPurpleBank.Api.Product.Model.Enum;

public enum EffectiveFrom
{
    [EnumMember(Value = "CURRENT")]
    Current,

    [EnumMember(Value = "FUTURE")]
    Future,

    [EnumMember(Value = "ALL")]
    All
}