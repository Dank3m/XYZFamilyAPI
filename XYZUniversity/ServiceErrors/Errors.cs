using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace XYZUniversity.ServiceErrors;

public static class Errors
{
    public static class Student
    {
        public static Error InvalidFirstName => Error.Validation(
            code: "Student.InvalidFirstName",
            description: $"Student first name must be at least {Models.Student.MinFirstNameLength}" +
                $" characters long and at most {Models.Student.MaxFirstNameLength} characters long.");

        public static Error InvalidLastName => Error.Validation(
            code: "Student.InvalidLastName",
            description: $"Student last name must be at least {Models.Student.MinLastNameLength}" +
                $" characters long and at most {Models.Student.MaxLastNameLength} characters long.");

        public static Error NotFound => Error.NotFound(
            code: "Student.NotFound",
            description: "Student not found");
    }

    public static class Payment
    {
        public static Error InvalidNarration => Error.Validation(
            code: "Payment.InvalidNarration",
            description: $"Payment narration must be at least {Models.Payment.MinNarrationLength}" +
                $" characters long and at most {Models.Payment.MaxNarrationLength} characters long.");

        public static Error NotFound => Error.NotFound(
            code: "Payment.NotFound",
            description: "Payment not found");
            
        public static Error AlreadyExists => Error.Conflict(
            code: "Payment.AlreadyExists",
            description: "Payment already exists");
    }

    public static class PaymentMethod
    {
        public static Error InvalidPaymentMethod => Error.NotFound(
            code: "PaymentMethod.InvalidPaymentMethod",
            description: "Payment method not found");
    }

    public static class PaymentChannel
    {
        public static Error InvalidPaymentChannel => Error.NotFound(
            code: "PaymentChannel.InvalidPaymentChannel",
            description: "Payment channel not found");
    }
}