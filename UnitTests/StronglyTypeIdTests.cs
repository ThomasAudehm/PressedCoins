using System.ComponentModel;
using PressedCoins.Domain.StronglyTypes;

namespace PressedCoins.UnitTests;

public class StronglyTypeIdTests
{
    const string TestGuid1 = "25141d12-1da4-41ce-b2ff-7a310d313215"; 
    const string TestGuid2 = "704c228c-406b-4737-9f22-28ebdfd97c44"; 
    
    [Fact]
    public void CanConvert_From_String()
    {
       
        var coin1 = new CoinId(new Guid(TestGuid1));
        var converter = TypeDescriptor.GetConverter(typeof(CoinId)); 
        
        var coin2 = (CoinId?)converter.ConvertFromString(TestGuid1); 
        
        Assert.Equal(coin1, coin2);
    }

    [Fact]
    public void Coins_Are_Not_Equal()
    {
        var coin1 = new CoinId(new Guid(TestGuid1));
        var coin2 = new CoinId(new Guid(TestGuid2));
        Assert.False(coin1.Equals(coin2));
    }
    
    [Fact]
    public void Throws_NotSupportedException()
    {
        var converter = TypeDescriptor.GetConverter(typeof(CoinId)); 
        Assert.Throws<NotSupportedException>( () => (CoinId?)converter.ConvertFromString("")); 
    }
}