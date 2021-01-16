using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Models;
using TestApp.ViewModels;

namespace TestApp.Profiles
{
    public class CiteProfiles : Profile
    {
        public CiteProfiles()
        {
            CreateMap<Product, ProductViewModel>()
                .ForMember(pvm => pvm.ProductName, p => p.MapFrom(p => p.Name))
                .ReverseMap();

            CreateMap<Supplier, SupplierViewModel>()
                .ForMember(svm => svm.OfficeAddress, s => s.MapFrom(s => s.Address))
                .ReverseMap();
        }
    }
}
