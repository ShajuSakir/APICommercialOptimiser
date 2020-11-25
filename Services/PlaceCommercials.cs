using APICommercialOptimiser.Entities;
using APICommercialOptimiser.Helpers;
using APICommercialOptimiser.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace APICommercialOptimiser.Services
{
    public class PlaceCommercials : IPlaceCommercials
    {
        List<BreakItems> breaks;
        List<CommercialItems> commercials;   
        List<CommercialBreak> response;        
        int break1Cap = 2;
        int break2Cap = 3;
        int break3Cap = 4;

        public PlaceCommercials()
        {
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    var basePath = Directory.GetCurrentDirectory();
                    //read breaks from JSON file
                    var breakFilePath = string.Concat(basePath, @"\Repo\breaks.json");
                    var breakJson = webClient.DownloadString(breakFilePath);
                    breaks = JsonConvert.DeserializeObject<List<BreakItems>>(breakJson);

                    //read commercials from JSON file
                    var commFilePath = string.Concat(basePath, @"\Repo\commercials.json");
                    var commJson = webClient.DownloadString(commFilePath);
                    commercials = JsonConvert.DeserializeObject<List<CommercialItems>>(commJson);

                }
                catch (Exception ex) { }
            }
        }

        public IEnumerable<CommercialBreak> GetCommercialsByUnevenStructure()
        {
            try
            {              
                //Organise Breaks
                int index = 0;
                breaks.OrderBy(x => x.Break);
                foreach (BreakItems brk in breaks)
                {
                    brk.Break = (index >=0 && index< 2) ? BreakTypesEnum.Break1 : (index >= 2 && index <5) ? BreakTypesEnum.Break2 : BreakTypesEnum.Break3;                  
                    index++;
                }
                response = OptimisedCommercials(true).ToList();
            }
            catch (Exception ex)
            { }

            return response;
        }
        public IEnumerable<CommercialBreak> GetCommercials(bool isOptimised)
        {
            try
            {
                if (isOptimised)
                {                    
                    response = OptimisedCommercials(false).ToList();
                }
                else
                {
                    GetCommercials();
                }

            }
            catch (Exception)
            { }

            return response;

        }

        public void GetCommercials()
        {
            try
            {
                response = new List<CommercialBreak>();
                CommercialBreak commercial;
                int commercialIndex = 0;
                foreach (BreakItems brk in breaks)
                {
                    commercial = new CommercialBreak
                    {
                        BreakName = (string.IsNullOrEmpty(Convert.ToString(brk.Break)) ? BreakTypesEnum.NoBreak : brk.Break),
                        //BreakName = brk.Break,
                        Demographic = brk.Demographic,
                        Rating = brk.Rating,
                        CommercialName = commercials.ElementAt(commercialIndex).CommercialName,
                        CommercialType = commercials.ElementAt(commercialIndex).CommercialType
                    };
                    commercialIndex++;
                    response.Add(commercial);
                }
               
            }
            catch (Exception)
            { }

            //return response;
        }

        public IEnumerable<CommercialBreak> OptimisedCommercials(bool isUneven)
        {
            try
            {
                response = new List<CommercialBreak>();
                List<CommercialBreak> tmpCommBrk = new List<CommercialBreak>();                
                CommercialBreak commercial;
                foreach (BreakItems brk in breaks)
                {                    
                    //find commercials with same demographic
                    List<CommercialItems> commByDemographic = (brk.Break != BreakTypesEnum.Break2) ? commercials.Where(x => x.Demographic == brk.Demographic && !x.isAllocated).ToList() :
                    commercials.Where(x => x.Demographic == brk.Demographic && x.CommercialType != CommercialTypeEnum.Finance && !x.isAllocated).ToList();
                  
                    var commTypeName = commByDemographic.Where(x => !x.isAllocated).FirstOrDefault().CommercialType;
                    var commName = commByDemographic.Where(x => !x.isAllocated).FirstOrDefault().CommercialName;
                    if (commByDemographic.Count() > 0 && ValidateSameTypeCommercialsInBreak1(commTypeName, brk.Break, isUneven))
                    {
                        commercial = new CommercialBreak
                        {
                            BreakName = brk.Break,
                            Demographic = brk.Demographic,
                            Rating = brk.Rating,
                            CommercialName = commByDemographic.Where(x => !x.isAllocated).FirstOrDefault().CommercialName,
                            CommercialType = commByDemographic.Where(x => !x.isAllocated).FirstOrDefault().CommercialType
                        };

                        response.Add(commercial);

                        commercials.Where(x => x.CommercialName == commName).FirstOrDefault().isAllocated = true;

                    }
                }
                GetOptimsedRatings(response);
            }
            catch (Exception ex)
            { }

            return response;
        }
      

        public void GetOptimsedRatings(List<CommercialBreak> optBreaks)
        {
            foreach (CommercialBreak brk in optBreaks)
            {
                foreach (var breakRatings in breaks)
                {
                    if (brk.BreakName == breakRatings.Break && brk.Demographic.ToString() == breakRatings.Demographic)
                    {
                        brk.OptimisedRating = breakRatings.Rating;
                    }
                }
            }
        }

        public bool ValidateSameTypeCommercialsInBreak1(CommercialTypeEnum tName, BreakTypesEnum brkName, bool isUnevn)
        {
            bool flag = true;
            int breakCapacity = (!isUnevn) ? 3 : (brkName == BreakTypesEnum.Break1) ? break1Cap : (brkName == BreakTypesEnum.Break2) ? break2Cap :
                (brkName == BreakTypesEnum.Break3) ? break3Cap : 0;
            List<CommercialBreak> breaks = response.Where(x => x.BreakName == brkName).ToList();
            if (breaks.Count == breakCapacity)
            {
                flag = false;
            }
            if (breaks.Count > 0)
            {
                int currentIndex = breaks.Count - 1;               
                int index = breaks.FindIndex(x => x.CommercialType == tName);
                if (index > -1)
                {
                    if (index == currentIndex)
                    {
                        if (index == 1)
                        {
                            //breaks.Reverse();
                            response.Move<CommercialBreak>(response.Count() - 1, response.Count() - 2);
                        }
                        else
                        {
                            flag = false;
                        }
                    }
                }
            }

            return flag;
        }
    }
}
