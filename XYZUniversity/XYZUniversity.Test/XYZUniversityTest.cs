using XYZUniversity.Data;
using XYZUniversity.Repositories.Payments;
using XYZUniversity.Repositories.Students;
using XYZUniversity.Models;
using System.Linq;
using Xunit;

namespace XYZUniversity.Test;

public class XYZUniversityTest
{
    [Fact]
    public void CreatePaymentTest()
    {
        Payment payment = Payment.Create(
            "Pamela - School Fees",
            10,
            DateTime.Now,
            "MOB",
            "MIB",
            5000,
            1,
            20).Value;

        Assert.NotNull(payment.PaymentRef);
        Assert.Equal(10, payment.BankRef);
        Assert.NotNull(payment);
       }

    [Fact]
    public void StudentCreateTest()
    {
        Student student = Student.Create(
            "Dan",
            "Kimolo",
            DateTime.Now,
            "Male",
            "$W",
            20).Value;

        Assert.NotNull(student.DateOfBirth);
        Assert.Equal(20, student.Id);
        Assert.NotNull(student);
    }
}