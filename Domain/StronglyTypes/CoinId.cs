using Vogen;

namespace PressedCoins.Domain.StronglyTypes;

[ValueObject<Guid>(Conversions.EfCoreValueConverter)]
public readonly partial struct CoinId;