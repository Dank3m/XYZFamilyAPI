namespace XYZUniversity.Contracts.Payment;

public record PaymentResponse(
    // Guid PaymentRef,
    int PaymentRef,
    string Narration,
    int BankRef,
    DateTime PaymentDate,
    string PaymentMethod,
    string PaymentChannel,
    double Amount,
    // Guid StudentId
    int StudentId
    );