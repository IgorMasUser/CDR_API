using CDR_API.Models;
using FluentValidation;

namespace CDR_API.Utils
{
    public class CallRecordValidator : AbstractValidator<CallRecord>
    {
        public CallRecordValidator()
        {
            RuleFor(x => x.CallerId)
                 .NotEmpty().WithMessage("Caller ID is required.")
                 .Length(1, 12).WithMessage("Caller ID length must be between 1 and 12 characters.");

            RuleFor(x => x.Recipient)
                .NotEmpty().WithMessage("Recipient is required.")
                .Length(1, 12).WithMessage("Recipient length must be between 1 and 12 characters.");

            RuleFor(x => x.CallDate)
                .NotEmpty().WithMessage("Call date is required.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("End time is required.");

            RuleFor(x => x.Duration)
                .GreaterThan(0).WithMessage("Duration must be greater than 0 seconds.");

            RuleFor(x => x.Cost)
                .GreaterThanOrEqualTo(0).WithMessage("Cost must be greater than or equal to 0.")
                .PrecisionScale(15, 3, true).WithMessage("Cost must not have more than 3 decimal places and be within 15 digits in total.");

            RuleFor(x => x.Reference)
                .NotEmpty().WithMessage("Reference is required.");

            RuleFor(x => x.Currency)
                .NotEmpty().WithMessage("Currency is required.")
                .Length(3).WithMessage("Currency code must be 3 characters long.");
        }
    }
}
