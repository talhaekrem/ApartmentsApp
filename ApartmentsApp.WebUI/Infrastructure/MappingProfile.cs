using ApartmentsApp.DB.Entities;
using ApartmentsApp.Models.Bills;
using ApartmentsApp.Models.Bills.CustomBills;
using ApartmentsApp.Models.Homes;
using ApartmentsApp.Models.Messages;
using ApartmentsApp.Models.Users;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApartmentsApp.WebUI.Infrastructure
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //house
            CreateMap<HomeAddModel, Homes>();
            CreateMap<Homes, HomeDetailsModel>();
            //CreateMap<Homes, HomeListModel>();

            //user
            CreateMap<UserAddModel, Users>();
            CreateMap<Users, UserDetailsModel>();
            CreateMap<UserUpdateModel, Users>();
            CreateMap<Users, AccountDetailsModel>();

            //message
            CreateMap<Messages, MessageListModel>();
            CreateMap<MessageSendModel, Messages>();
            CreateMap<Messages, MessageSendModel>();
            CreateMap<Messages, MessageDetailModel>();
            //custom bills aidat-kira,elektrik,su,doğalgaz
            //ekle
            CreateMap<BillsAddModel, HomeBill>();
            CreateMap<BillsAddModel, ElectricBill>();
            CreateMap<BillsAddModel, WaterBill>();
            CreateMap<BillsAddModel, GasBill>();
            //detay
            CreateMap<HomeBill, BillsDetailsModel>();
            CreateMap<ElectricBill, BillsDetailsModel>();
            CreateMap<WaterBill, BillsDetailsModel>();
            CreateMap<GasBill, BillsDetailsModel>();
            //güncelle
            CreateMap<BillsUpdateModel, HomeBill>();
            CreateMap<BillsUpdateModel, ElectricBill>();
            CreateMap<BillsUpdateModel, WaterBill>();
            CreateMap<BillsUpdateModel, GasBill>();

        }
    }
}
