namespace Ordering.Application.Dtos;
public record AddressDto(
       string FirstName,
       string LastName,
       string EmailAddress,
       string Address,
       string Country,
       string State,
       string ZipCode);
