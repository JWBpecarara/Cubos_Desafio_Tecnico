using System.Runtime.Serialization;

namespace CubosFinancialAPI.Enum
{
    public enum CardType
    {
        [EnumMember(Value = "physical")]
        physical,

        [EnumMember(Value = "virtual")]
        virtual_card
    }
}
