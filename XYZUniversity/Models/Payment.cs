using XYZUniversity.Contracts.Payment;
using XYZUniversity.ServiceErrors;
using ErrorOr;

namespace XYZUniversity.Models;

public class Payment
{
    public const int MinNarrationLength = 3;
    public const int MaxNarrationLength = 30;

    // public Guid PaymentRef { get; }
    public int PaymentRef {get; }
    public string Narration { get; }
    public int BankRef{ get;}
    public DateTime PaymentDate { get; }
    public string PaymentMethod_ { get; }
    public string PaymentChannel_ { get; }
    public double Amount { get; }
    // public Guid StudentId { get; }
    public int StudentId { get; }
    public virtual Student Student {get;}

    public virtual PaymentMethod PaymentMethod {get;}
    public virtual PaymentChannel PaymentChannel {get;}

    public Payment(){
        
    }

    private Payment(
        // Guid paymentRef,
        int paymentRef,
        string narration,
        int bankRef,
        DateTime paymentDate,
        string paymentMethod,
        string paymentChannel,
        double amount,
        // Guid studentId
        int studentId)
    {
        PaymentRef = paymentRef;
        Narration = narration;
        BankRef = bankRef;
        PaymentDate = paymentDate;
        PaymentMethod_ = paymentMethod;
        PaymentChannel_ = paymentChannel;
        Amount = amount;
        StudentId = studentId;
    }

    public static ErrorOr<Payment> Create(
        string narration,
        int bankRef,
        DateTime paymentDate,
        string paymentMethod,
        string paymentChannel,
        double amount,
        // Guid studentId,
        // Guid? paymentRef = null
        int studentId,
        int paymentRef)
    {
        List<Error> errors = new();

        if (narration.Length is < MinNarrationLength or > MaxNarrationLength)
        {
            errors.Add(Errors.Payment.InvalidNarration);
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return new Payment(
            // paymentRef ?? Guid.NewGuid(),
            paymentRef,
            narration,
            bankRef,
            paymentDate,
            paymentMethod,
            paymentChannel,
            amount,
            studentId);
    }

    public static ErrorOr<Payment> From(CreatePaymentRequest request)
    {
        return Create(
            request.Narration,
            request.BankRef,
            request.PaymentDate,
            request.PaymentMethod,
            request.PaymentChannel,
            request.Amount,
            request.StudentId,
            request.PaymentRef);
    }

    public static ErrorOr<Payment> From(//Guid paymentRef,
                                            int paymentRef,
                                             UpsertPaymentRequest request)
    {
        return Create(
            request.Narration,
            request.BankRef,
            request.PaymentDate,
            request.PaymentMethod,
            request.PaymentChannel,
            request.Amount,
            request.StudentId,
            paymentRef);
    }
}