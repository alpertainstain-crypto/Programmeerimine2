using KooliProjekt.Application.Data;
using MediatR;
using System.Collections.Generic;

public class GetUsers : IRequest<List<User>>
{
}
