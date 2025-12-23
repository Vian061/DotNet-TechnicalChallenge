namespace HealthCare.Application.ValueObjects
{
    public record AvailabilitySlot(
        DateTime StartUtc,
        DateTime EndUtc
    );
}
