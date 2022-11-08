using Application.Features.Brands.Dtos;
using Application.Features.Brands.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Brands.Commands.CreateBrand
{
    public class CreateBrandComment:IRequest<CreatedBrandDto>
    {
        public string Name { get; set; }
        public class CreateBrandCommentHandler : IRequestHandler<CreateBrandComment, CreatedBrandDto>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IMapper _mapper;
            private readonly BrandBusinessRules _brandBusinessRules;
            public CreateBrandCommentHandler(IBrandRepository brandRepository, IMapper mapper,
                BrandBusinessRules brandBusinessRules)
            {
                _brandRepository = brandRepository;
                _mapper = mapper;
                _brandBusinessRules = brandBusinessRules;
            }

            public async Task<CreatedBrandDto> Handle(CreateBrandComment request, CancellationToken cancellationToken)
            {
                await _brandBusinessRules.BrandNameCanNotBeDuplicatedWhenInserted(request.Name);    
                //autoMapper kütüphanedsinden gelen mapper ile burada brand'den gelen nesneleri dto'ya mappliyoruz
                //ve direk veri tabanı nesnelreine bağlı değiliz.mapper burada bizim için burada eşleşritmeyi yapıyor.
               Brand mappedBrand=_mapper.Map<Brand>(request);
               Brand CreateBrand = await _brandRepository.AddAsync(mappedBrand);
               CreatedBrandDto createdBrandDto = _mapper.Map<CreatedBrandDto>(CreateBrand);
                return createdBrandDto;
            }
        }
    }
}
