using XYZUniversity.Models;
using XYZUniversity.Repositories.Payments;
using XYZUniversity.ServiceErrors;
using ErrorOr;

namespace XYZUniversity.Services.Payments;

public class PaymentService : IPaymentService
{

    private readonly IPaymentRepository _paymentRepo;

    public PaymentService (IPaymentRepository paymentRepo)
    {
        _paymentRepo = paymentRepo;
    }

    // private static readonly Dictionary<Guid, Payment> _payments = new();
    private static readonly Dictionary<int, Payment> _payments = new();

    public ErrorOr<Created> CreatePayment(Payment payment)
    {
        // ErrorOr<Student> student = _studentService.GetStudent(payment.StudentId);
        
        // _payments.Add(payment.PaymentRef, payment);
        // if(student.IsError) {
        //     return Errors.Student.NotFound;
        // }
        //In Memory
        // return _paymentRepo.CreatePayment(payment);
        //For Database
        return _paymentRepo.CreatePaymentDB(payment);
        
    }

    public ErrorOr<Deleted> DeletePayment(//Guid paymentRef
                                            int paymentRef)
    {
        //In Memory
        // return _paymentRepo.DeletePayment(paymentRef);
        //For Database
        return _paymentRepo.DeletePaymentDB(paymentRef);
    }

    public ErrorOr<Payment> GetPayment(//Guid paymentRef
                                            int paymentRef)
    {
        //In Memory
        // return _paymentRepo.GetPayment(paymentRef);
        //For Database
        return _paymentRepo.GetPaymentDB(paymentRef);
    }

    public ErrorOr<UpsertedPayment> UpsertPayment(Payment payment)
    {
        //In Memory
        // return _paymentRepo.UpsertPayment(payment);
        //For Database
        return _paymentRepo.UpsertPaymentDB(payment);
    }
}