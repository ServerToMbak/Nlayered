using Application.Features.Models.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Models.Queries.GetListModelByQuery
{
    public class GetListModelBYDynamicQuery : IRequest<ModelListModel>
    {
        public Dynamic Dynamic { get; set; }
        public PageRequest PageRequest { get; set; }

        public class GetListModelByFynamicQueryHandler : IRequestHandler<GetListModelBYDynamicQuery, ModelListModel>
        {
            private readonly IMapper _mapper;
            private readonly IModelRepository _modelRepository;

            public GetListModelByFynamicQueryHandler(IMapper mapper, IModelRepository modelRepository)
            {
                _mapper = mapper;
                _modelRepository = modelRepository;
            }

            public async Task<ModelListModel> Handle(GetListModelBYDynamicQuery request, CancellationToken cancellationToken)
            {

                IPaginate<Model> models = await _modelRepository.GetListByDynamicAsync(request.Dynamic,include:
                                            m => m.Include(c => c.brand),
                                            index: request.PageRequest.Page,
                                            size: request.PageRequest.PageSize

                                            );
                ModelListModel Mappedmodels = _mapper.Map<ModelListModel>(models);
                return Mappedmodels;
            }
        }

    }
}
