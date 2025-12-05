using KooliProjekt.Application.Data;
using MediatR;
using System.Collections.Generic;

public class GetAvailability : IRequest<List<Availability>>
{
}
