using XYZUniversity.Models;
using XYZUniversity.Repositories.Payments;
using ErrorOr;

namespace XYZUniversity.Services.Payments;

public interface IPaymentService
{
    ErrorOr<Created> CreatePayment(Payment payment);
    ErrorOr<Payment> GetPayment(//Guid paymentRef
                                int paymentRef);
    ErrorOr<UpsertedPayment> UpsertPayment(Payment payment);
    ErrorOr<Deleted> DeletePayment(//Guid paymentRef
                                    int paymentRef);
}