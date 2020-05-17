using System;
using System.Collections.Generic;
using System.Text;

namespace MYMLibrary
{
    public class PlaceModel
    {
        private int ID { get; set; }
        private String City { get; set; }
        private String PostCode { get; set; }
        private String Street { get; set; }
        private String Description { get; set; }


        public PlaceModel()
        {

        }

        public PlaceModel(/*int IDValue, */ String CityValue, String PostCodeValue, String StreetValue, String DescriptionValue)
        {
            //ID = IDValue;

            City = CityValue;
            PostCode = PostCodeValue;
            Street = StreetValue;
            if (DescriptionValue == null || DescriptionValue == "")
                Description = "Empty";
            else
                Description = DescriptionValue;
        }

        public PlaceModel(int IDValue, String CityValue, String PostCodeValue, String StreetValue, String DescriptionValue)
        {
            ID = IDValue;
            City = CityValue;
            PostCode = PostCodeValue;
            Street = StreetValue;
            if (DescriptionValue == null || DescriptionValue == "")
                Description = "Empty";
            else
                Description = DescriptionValue;
        }

        ~PlaceModel()
        {

        }

        public void setID(int newID)
        {
            this.ID = newID;
        }

        public int getID()
        {
            return this.ID;
        }

        public void setCity(String newCity)
        {
            this.City = newCity;
        }
        public String getCity()
        {
            return this.City;
        }

        public void setPostCode(String newPostCode)
        {
            this.PostCode = newPostCode;
        }
        public String getPostCode()
        {
            return this.PostCode;
        }

        public void setStreet(String newStreet)
        {
            this.Street = newStreet;
        }
        public String getStreet()
        {
            return this.Street;
        }

        public void setDescription(String newDescription)
        {
            this.Description = newDescription;
        }
        public String getDescription()
        {
            return this.Description;
        }


    }
}
