namespace XYZUniversity.Models;

public class PaymentMethod
{
    public string Method { get; }
    public string PaymentMethodDescription { get; }

    public virtual List<Payment> Payments {get; } 

    public PaymentMethod(){
        
    }

    public PaymentMethod(
        string paymentMethod,
        string paymentMethodDescription)
    {
        Method = paymentMethod;
        PaymentMethodDescription = paymentMethodDescription;
    }

    
}