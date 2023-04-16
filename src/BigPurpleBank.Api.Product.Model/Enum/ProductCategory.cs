using System.Runtime.Serialization;

namespace BigPurpleBank.Api.Product.Model.Enum;

public enum ProductCategory
{
    [EnumMember(Value = "BUSINESS_LOANS")]
    BusinessLoans,

    [EnumMember(Value = "CRED_AND_CHRG_CARDS")]
    CreditAndChargeCards,

    [EnumMember(Value = "LEASES")]
    Leases,

    [EnumMember(Value = "MARGIN_LOANS")]
    MarginLoans,

    [EnumMember(Value = "OVERDRAFTS")]
    OverDrafts,

    [EnumMember(Value = "PERS_LOANS")]
    PersonalLoans,

    [EnumMember(Value = "REGULATED_TRUST_ACCOUNTS")]
    RegulatedTrustAccounts,

    [EnumMember(Value = "RESIDENTIAL_MORTGAGES")]
    ResidentialMortgages,

    [EnumMember(Value = "TERM_DEPOSITS")]
    TermDeposits,

    [EnumMember(Value = "TRADE_FINANCE")]
    TradeFinance,

    [EnumMember(Value = "TRANS_AND_SAVINGS_ACCOUNTS")]
    TransactionsAndSavingsAccount,

    [EnumMember(Value = "TRAVEL_CARDS")]
    TravelCards
}