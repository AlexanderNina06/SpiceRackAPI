using System;
using MediatR;
using SpiceRack.Core.Application.DTOs.Responses.TableResponses;
using SpiceRack.Core.Application.Wrappers;

namespace SpiceRack.Core.Application.Features.Tables.Queries;

public class GetAllTablesQuery : IRequest<Response<IList<GetTableResponse>>>
{

}
