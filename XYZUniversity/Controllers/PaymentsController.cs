using XYZUniversity.Contracts.Payment;
using XYZUniversity.Models;
using XYZUniversity.Repositories.Payments;
using XYZUniversity.Services.Payments;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace XYZUniversity.Controllers;

public class PaymentsController : ApiController
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpPost]
    public IActionResult CreatePayment(CreatePaymentRequest request)
    {
        ErrorOr<Payment> requestToPaymentResult = Payment.From(request);

        if (requestToPaymentResult.IsError)
        {
            return Problem(requestToPaymentResult.Errors);
        }

        var payment = requestToPaymentResult.Value;
        ErrorOr<Created> createPaymentResult = _paymentService.CreatePayment(payment);

        return createPaymentResult.Match(
            created => CreatedAtGetPayment(payment),
            errors => Problem(errors));
    }

    [HttpGet("{id:int}")]
    public IActionResult GetPayment(//Guid id
                                    int id)
    {
        ErrorOr<Payment> getPaymentResult = _paymentService.GetPayment(id);

        return getPaymentResult.Match(
            payment => Ok(MapPaymentResponse(payment)),
            errors => Problem(errors));
    }

    [HttpPut("{id:int}")]
    public IActionResult UpsertPayment(//Guid id,
                                        int id,
                                         UpsertPaymentRequest request)
    {
        ErrorOr<Payment> requestToPaymentResult = Payment.From(id, request);

        if (requestToPaymentResult.IsError)
        {
            return Problem(requestToPaymentResult.Errors);
        }

        var payment = requestToPaymentResult.Value;
        ErrorOr<UpsertedPayment> upsertPaymentResult = _paymentService.UpsertPayment(payment);

        return upsertPaymentResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetPayment(payment) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:int}")]
    public IActionResult DeletePayment(//Guid id
                                        int id)
    {
        ErrorOr<Deleted> deletePaymentResult = _paymentService.DeletePayment(id);

        return deletePaymentResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }

    private static PaymentResponse MapPaymentResponse(Payment payment)
    {
        return new PaymentResponse(
            payment.PaymentRef,
            payment.Narration,
            payment.BankRef,
            payment.PaymentDate,
            payment.PaymentMethod_,
            payment.PaymentChannel_,
            payment.Amount,
            payment.StudentId);
    }

    private CreatedAtActionResult CreatedAtGetPayment(Payment payment)
    {
        return CreatedAtAction(
            actionName: nameof(GetPayment),
            routeValues: new { id = payment.PaymentRef },
            value: MapPaymentResponse(payment));
    }
}