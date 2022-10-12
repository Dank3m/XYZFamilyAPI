namespace XYZUniversity.Contracts.Payment;

public record UpsertPaymentRequest(
    string Narration,
    int BankRef,
    DateTime PaymentDate,
    string PaymentMethod,
    string PaymentChannel,
    double Amount,
    // Guid StudentId
    int StudentId,
    int PaymentRef);