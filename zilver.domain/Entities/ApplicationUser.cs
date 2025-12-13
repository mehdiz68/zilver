using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Mail;

namespace zilver.domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        // هر فیلد اضافی که لازم باشه اینجا اضافه کن
        [MaxLength(100, ErrorMessage = "حداکثر طول کارکتر ، 15")]
        public string? FirstName { get; set; }
        [MaxLength(100, ErrorMessage = "حداکثر طول کارکتر ، 15")]
        public string? LastName { get; set; }
        public bool Gender { get; set; }
        public bool Disable { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid? Avatar { get; set; }
        public Attachment? Avatarattachment { get; set; }
        public string? About { get; set; }
        public string? LandlinePhone { get; set; }
        public string? PostalCode { get; set; }
        public string? Address { get; set; }
        public string? AddressNumber { get; set; }
        public string? AddressUnit { get; set; }
        public string? NationalCode { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? CardNumber { get; set; }
        public string? SHBNumber { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? LastLoggedIn { get; set; }
        public int CityId { get; set; }
        public City CityEntity { get; set; } = null!;
        public string? ActiveTempCode { get; set; }
        public DateTime? ActiveTempCodeExpire { get; set; }
        public bool Haghighi { get; set; }
        public string? CompanyName { get; set; }
        public long? CompanyCode { get; set; }
        public long? CompanyRegisterCode { get; set; }
        public long? CompanyTaxCode { get; set; }
        public bool CodeMatched { get; set; }
        public DateTime? CodeMatchedExpire { get; set; }
        public int CodeMatchedFailedCount { get; set; }
        public string? CodeMatchedLog { get; set; }

       // public UserActivityReferer UserActivityReferer { get; set; }
        public int? UserActivityRefererId { get; set; }

        public bool Publisher { get; set; }
        public bool PublisherVerified { get; set; }
        public string? PublisherFullName { get; set; }
        public string? PublisherDescr { get; set; }

        public ICollection<RefreshToken>? RefreshTokens { get; set; }
        public ICollection<Product>? Products { get; set; }
        public ICollection<Attachment> Attachements { get; set; }=null!;
        //public ICollection<Padv> Padvs { get; set; }
        //public ICollection<SearchLog> SearchLogs { get; set; }
        //public ICollection<AdministratorPermission> AdministratorPermissions { get; set; }
        //public ICollection<Content> Contents { get; set; }
        //public ICollection<EventLog> EventLogs { get; set; }
        //public ICollection<ProductQuestion> ProductQuestions { get; set; }
        //public ICollection<ProductComment> ProductComments { get; set; }
        //public ICollection<ProductLetmeknow> ProductLetmeknows { get; set; }
        //public ICollection<ProductFavorate> ProductFavorates { get; set; }
        //public ICollection<ProductRankSelectValue> ProductRankSelectValues { get; set; }
        //public ICollection<Wallet> Wallets { get; set; }
        //public ICollection<dWallet> dWallets { get; set; }
        //public ICollection<UserAddress> UserAddresses { get; set; }
        //public ICollection<UserMessage> UserMessages { get; set; }
        //public ICollection<UserMessage> UserMessage2s { get; set; }
        //public ICollection<UserBon> UserBons { get; set; }
        //public ICollection<Seller> Sellers { get; set; }
        //public ICollection<UserCodeGift> UserCodeGifts { get; set; }
        //public ICollection<CourseRating> CourseRatings { get; set; }
        //public ICollection<Offer> Offers { get; set; }
        //public ICollection<Order> Orders { get; set; }
        //public ICollection<ProductPriceGroupModification> ProductPriceGroupModifications { get; set; }
        //public ICollection<UserGroupSelect> UserGroupSelects { get; set; }
        //public ICollection<UserOfferMessageMember> UserOfferMessageMembers { get; set; }
    }
}
