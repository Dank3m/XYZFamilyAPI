using Microsoft.EntityFrameworkCore;
using XYZUniversity.Data;
using XYZUniversity.Models;
using XYZUniversity.ServiceErrors;
using ErrorOr;

namespace XYZUniversity.Repositories.Payments;

public class PaymentRepository : IPaymentRepository
{
    //In memory retrieval
    // private static readonly Dictionary<Guid, Student> _payments = new();
    private static readonly Dictionary<int, Payment> _payments = new();

    public ErrorOr<Created> CreatePayment(Payment payment)
    {
        // _payments.Add(student.Id, student);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeletePayment(//Guid id
                                            int id)
    {
        // _payments.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Payment> GetPayment(//Guid id
                                            int id)
    {
        // if (_payments.TryGetValue(id, out var student))
        // {
        //     return student;
        // }

        return Errors.Student.NotFound;
    }

    public ErrorOr<UpsertedPayment> UpsertPayment(Payment payment)
    {
        var isNewlyCreated = !_payments.ContainsKey(payment.PaymentRef);
        // _payments[payment.PaymentRef] = payment;

        return new UpsertedPayment(isNewlyCreated);
    }

    //Database Retrieval

    private readonly DataContext _context;

    public PaymentRepository(DataContext context)
    {
        _context = context;
    }

    public ErrorOr<Created> CreatePaymentDB(Payment payment)
    {
        Console.WriteLine(payment.Narration);
        int count = 0;
        try {
            ErrorOr<Student> student = (from s in _context.Students where s.Id 
                        == payment.StudentId select s).First<Student>();
            count++;
            ErrorOr<PaymentMethod> method = (from pm in _context.PaymentMethods where pm.Method 
                        == payment.PaymentMethod_ select pm).First<PaymentMethod>();
            count++;
            ErrorOr<PaymentChannel> channel = (from pc in _context.PaymentChannels where pc.Channel 
                        == payment.PaymentChannel_ select pc).First<PaymentChannel>();
            count++;
            ErrorOr<Payment> paymentDB = (from p in _context.Payments where p.BankRef
                        == payment.BankRef select p).First<Payment>();
            Console.WriteLine(payment.BankRef);
            count++;

            
            }
            
        catch (Exception ex) {
            ex.GetBaseException();
            if (count == 0) {
                return Errors.Student.NotFound;
            }
            else if (count == 1) {   
            return Errors.PaymentMethod.InvalidPaymentMethod;
            }
            else if (count == 2) {   
            return Errors.PaymentChannel.InvalidPaymentChannel;
            }
            else if (count == 3) {             
            _context.Payments.Add(payment);
            _context.SaveChanges();
            return Result.Created;
            }
        }
        return Errors.Payment.AlreadyExists;
    }

    public ErrorOr<Deleted> DeletePaymentDB(//Guid id
                                            int id)
    {
        try {
            ErrorOr<Payment> payment = (from p in _context.Payments where p.PaymentRef 
            == id select p).First<Payment>();
            _context.Payments.Remove(payment.Value);
            _context.SaveChanges();
        }

        catch (Exception ex)
        {
            ex.GetBaseException();
            return Errors.Payment.NotFound;
        }

        return Result.Deleted;
    }

    public ErrorOr<Payment> GetPaymentDB(//Guid id
                                            int id)
    {
        try{
        ErrorOr<Payment> payment = (from p in _context.Payments where p.PaymentRef
            == id select p).First<Payment>();
            return payment;
        }

        catch (Exception ex)
        {
            ex.GetBaseException();
            return Errors.Payment.NotFound;
        }


            
    }

    public ErrorOr<UpsertedPayment> UpsertPaymentDB(Payment payment)
    {
        var isNewlyCreated = false;
        int count = 0;
        try{     
            ErrorOr<Student> student = (from s in _context.Students where s.Id 
                == payment.StudentId select s).First<Student>();
            count++;
            ErrorOr<PaymentMethod> method = (from pm in _context.PaymentMethods where pm.Method 
                        == payment.PaymentMethod_ select pm).First<PaymentMethod>();
            count++;
            ErrorOr<PaymentChannel> channel = (from pc in _context.PaymentChannels where pc.Channel 
                        == payment.PaymentChannel_ select pc).First<PaymentChannel>();
            count++;

            ErrorOr<Payment> pay = (from p in _context.Payments where p.BankRef
            == payment.BankRef || p.PaymentRef == payment.PaymentRef select p).First<Payment>();
            count++;
            Console.WriteLine(payment);

            
            _context.Payments.Update(payment);
            _context.SaveChanges();
            count++;
            

            return new UpsertedPayment(isNewlyCreated);
            
        }  
        catch(DbUpdateException ex) {
            ex.GetBaseException();
            return Errors.Payment.AlreadyExists;
        }

        catch (Exception ex) {
            ex.GetBaseException();
            Console.WriteLine("*****************COUNT************");
            Console.WriteLine(count);
            if (count == 0)
                return Errors.Student.NotFound;
            if (count == 1)
                return Errors.PaymentMethod.InvalidPaymentMethod;
            if (count == 2)
                return Errors.PaymentChannel.InvalidPaymentChannel;
            if (count == 3) {
                _context.Entry(payment).State = EntityState.Detached;
                _context.Payments.Update(payment);
                _context.SaveChanges();
                return new UpsertedPayment(isNewlyCreated);
            }

                
            if (count == 4) {
                Console.WriteLine("hEREEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE");
                _context.Entry(payment).State = EntityState.Detached;
                return Errors.Payment.AlreadyExists;
            }

            isNewlyCreated = true;
            _context.Entry(payment).State = EntityState.Detached;
                _context.Payments.Add(payment);
            _context.SaveChanges();
            
        }
        return new UpsertedPayment(isNewlyCreated);
    }

}