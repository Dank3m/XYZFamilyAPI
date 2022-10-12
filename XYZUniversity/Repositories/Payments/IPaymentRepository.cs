using XYZUniversity.Models;
using ErrorOr;

namespace XYZUniversity.Repositories.Payments;

public interface IPaymentRepository
{
    ErrorOr<Created> CreatePayment(Payment payment);
    ErrorOr<Payment> GetPayment(//Guid id,
                                    int id);
    ErrorOr<UpsertedPayment> UpsertPayment(Payment payment);
    ErrorOr<Deleted> DeletePayment(//Guid id
                                        int id);
    ErrorOr<Created> CreatePaymentDB(Payment payment);
    ErrorOr<Payment> GetPaymentDB(//Guid id,
                                    int id);
    ErrorOr<UpsertedPayment> UpsertPaymentDB(Payment payment);
    ErrorOr<Deleted> DeletePaymentDB(//Guid id
                                        int id);
}
