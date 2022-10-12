namespace XYZUniversity.Models;

public class PaymentChannel
{
    public string Channel { get; }
    public string PaymentChannelDescription { get; }

    public virtual List<Payment> Payments {get; } 

    public PaymentChannel(){
        
    }

    public PaymentChannel(
        string paymentChannel,
        string paymentChannelDescription)
    {
        Channel = paymentChannel;
        PaymentChannelDescription = paymentChannelDescription;
    }

    
}