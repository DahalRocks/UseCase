using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for GetWikiText
/// </summary>
public class GetWikiText
{

    public string wikitext { get; set; }
    public GetWikiText(XMLParser xdata)
    {


        List<DescriptionSecondTable> dtLst = xdata.lstTable;
        List<TechnicalDetailFirstTable> dtTechFirst = xdata.lstTechTable;
        List<StudyFirstTable> dtStudyFirst = xdata.lstStudyTable;
        List<StudySecondTable> dtStudySecond = xdata.lstStudySecondTable;
        List<StudySecondTable> dtStudyThird = xdata.lstStudyThirdTable;

        List<TechnicalDetailSecondTable> dtTechSecond = xdata.lstTechSecondTable;
        List<TechnicalDetailThirdTable> dtTechThird = xdata.lstTechThirdTable;

        List<InfoFirstTable> dtInfo = xdata.lstInfoTable;

        List<TechnicalRequirement> dtReq = xdata.lstTechnicalReq;
        List<TechnicalRequirementSecondPart> dtReqSecond = xdata.lstTechnicalReqsecond;

        List<TermsAndDefinitions> dtTerms = xdata.lstTermDefinition;



        string general_description = "= Description of use case =" + "\n";
        general_description += "== Name of Use Case==" + "\n";


        //................................create first table..........................................................................//    
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! ID !! Area domain !! Name" + "\n";
        general_description += "|-" + "\n";
        general_description += "| " + xdata.firsttable.ID + "|| " + xdata.firsttable.Areadomain + "||" + xdata.firsttable.Name + " \n";
        general_description += "|-" + "\n";
        general_description += "|}" + "\n";
        //..............................................................................................................................//
        general_description += "==Version Management==" + "\n";

        //................................create second table...........................................................................// 
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Version !! Date !! Auther Name/Committe !! Changes!! UseCaseStatus" + "\n";
        general_description += "|-" + "\n";
        foreach (DescriptionSecondTable lst in dtLst)
        {
            general_description += "|" + lst.Version + "||" + lst.Date + "||" + lst.AuthorNameCommitte + "||" + lst.Changes + "||" + lst.UseCaseStatus + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";
        //..................................................................................................................................

        general_description += "== Usecasets goals, purpose, application==" + "\n";
        //...............................1.3	Usecasets mål, hensikt, anvendelse....................................................................................................//
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "!Beskrivelse av mål og hensikt med funksjonaliteten til use caset" + "\n";

        general_description += "|-" + "\n";
        general_description += "| Area,extent ||" + xdata.thirdtable.Area + "\n";

        general_description += "|-" + "\n";
        general_description += "| Goals ||" + xdata.thirdtable.Goal + "\n";
        general_description += "|-" + "\n";

        general_description += "| Related Business Case ||" + xdata.thirdtable.RelatedBusinessCase + "\n";
        general_description += "|-" + "\n";
        general_description += "|}" + "\n";
        //...........................................................................................................................................................

        general_description += "== Use case description and narrative==" + "\n";
        //...............................1.4	Use case beskrivelse og narrativ....................................................................................................//
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Usecase description" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Short description" + "\n";
        general_description += "|-" + "\n";
        general_description += "| " + xdata.fourthtable.ShortDescription + " \n";
        general_description += "|-" + "\n";
        general_description += "! Complete description" + "\n";
        general_description += "|-" + "\n";
        general_description += "| " + xdata.fourthtable.CompleteDescription + " \n";
        general_description += "|}" + "\n";
        //.............................................................................................................................................................
        general_description += "==Any comments ==" + "\n";

        //................................1.5 Eventuelle kommentarer...................................................................................

        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Any comments" + "\n";
        general_description += "|-" + "\n";
        general_description += "| " + xdata.fifthtable.AnyComments + " \n";
        general_description += "|-" + "\n";
        general_description += "|}" + "\n";






        //...............................2.o Technical details.................................................................................
        general_description += "=Technical detail=" + "\n";


        general_description += "==Stakeholders: People, systems, applications, databases, systems, components, equipment and other stakeholders==" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Player name !! Performer type !! Performer description !! Additional information for this specific use case study" + "\n";
        general_description += "|-" + "\n";
        foreach (TechnicalDetailFirstTable lst in dtTechFirst)
        {
            general_description += "|" + lst.PlayerName + "||" + lst.PerformerType + "||" + lst.PerformerDescription + "||" + lst.AdditionalInfo + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";

        //..............................2.1 Assumptions, assumptions, events.......................................................................
        general_description += "==Assumptions,events==" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Actor / System / Info / Contract !! Triggering event !! Start conditions !! Assumptions" + "\n";
        general_description += "|-" + "\n";
        foreach (TechnicalDetailSecondTable lst in dtTechSecond)
        {
            general_description += "|" + lst.Actor + "||" + lst.TriggeringEvent + "||" + lst.StartConditions + "||" + lst.Assumptions + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";
        //..............................2.3 References.......................................................................
        general_description += "==References==" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! No. !! Reference type !! Reference !! Status !! Implications for Use case study !! Parentage / organization!!Link" + "\n";
        general_description += "|-" + "\n";
        foreach (TechnicalDetailThirdTable lst in dtTechThird)
        {
            general_description += "|" + lst.No + "||" + lst.ReferenceType + "||" + lst.Reference + "||" + lst.Status + "||" + lst.Implications + "||" + lst.Organization + "||" + lst.Link + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";
        //..............................2.4 Information on Use Case.......................................................................
        general_description += "==Information on Use Case==" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Information classification" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Possible relation to other use case" + "\n";
        general_description += "|-" + "\n";
        general_description += "|" + xdata.techfourthtable.RelationToOther + "\n";
        general_description += "|-" + "\n";
        general_description += "! Level / depth" + "\n";
        general_description += "|-" + "\n";
        general_description += "|" + xdata.techfourthtable.Level + "\n";
        general_description += "|-" + "\n";
        general_description += "! Priorities: (mandatory / required, internal prioritization, timing ...)" + "\n";
        general_description += "|-" + "\n";
        general_description += "|" + xdata.techfourthtable.Priorities + "\n";
        general_description += "|-" + "\n";
        general_description += "! Generic, regional or national interest / application" + "\n";
        general_description += "|-" + "\n";
        general_description += "|" + xdata.techfourthtable.Interest + "\n";
        general_description += "|-" + "\n";
        general_description += "! Orientation - Technically oriented, business oriented ..." + "\n";
        general_description += "|-" + "\n";
        general_description += "|" + xdata.techfourthtable.Orientation + "\n";
        general_description += "|-" + "\n";
        general_description += "! Key words (for search, classification)" + "\n";
        general_description += "|-" + "\n";
        general_description += "|" + xdata.techfourthtable.KeyWords + "\n";
        general_description += "|-" + "\n";
        general_description += "|}" + "\n";

        //.............................3.1 Usecase study step by step..........................................................................
        general_description += "=Usecase study step by step=" + "\n";


        general_description += "==Scenario Name==" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! No. !! Scenario name !! Primary actor !! Trigerring event !! Start condition!! Final terms" + "\n";
        general_description += "|-" + "\n";
        foreach (StudyFirstTable lst in dtStudyFirst)
        {
            general_description += "|" + lst.No + "||" + lst.Notification + "||" + lst.PrimaryActor + "||" + lst.TriggeringEvent + "||" + lst.StartCondition + "||" + lst.FinalTerms + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";
        //.............................3.2 Step scenario..........................................................................
        general_description += "==Step scenario==" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Scenario Name: !!  Notification of durable high / low voltage" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Step ranked !! Hend-else !! Name of Process / Activity !! Description of Process / Activity !! Service !! Information creates !! Information Receiver !! Information exchanged !! Technical requirements (R-ID)" + "\n";
        general_description += "|-" + "\n";
        foreach (StudySecondTable lst in dtStudySecond)
        {
            general_description += "|" + lst.StepRanked + "||" + lst.HendElse + "||" + lst.Activity + "||" + lst.ProcessDescription + "||" + lst.Service + "||" + lst.InformationCreates + "||" + lst.InformationReceiver + "||" + lst.InformationExchanged + "||" + lst.TechnicalRequirements + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";
        //.............................3.3 Step scenario..........................................................................
        if (dtStudyThird != null)
        {
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Scenario Name: !!  Ongoing collection of voltage data" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Step ranked !! Hend-else !! Name of Process / Activity !! Description of Process / Activity !! Service !! Information creates !! Information Receiver !! Information exchanged !! Technical requirements (R-ID)" + "\n";
        general_description += "|-" + "\n";
        

            foreach (StudySecondTable lst in dtStudyThird)
            {
                general_description += "|" + lst.StepRanked + "||" + lst.HendElse + "||" + lst.Activity + "||" + lst.ProcessDescription + "||" + lst.Service + "||" + lst.InformationCreates + "||" + lst.InformationReceiver + "||" + lst.InformationExchanged + "||" + lst.TechnicalRequirements + "\n";
                general_description += "|-" + "\n";
            }
        
        
        general_description += "|}" + "\n";
        }
        //.............................4.1 Information to be exchanged..........................................................................
        general_description += "=Information to be exchanged=" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! More about information exchanged" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Name of information (ID) !!  Description of the information !! Required information / data" + "\n";
        general_description += "|-" + "\n";
        foreach (InfoFirstTable lst in dtInfo)
        {
            general_description += "|" + lst.ID + "||" + lst.Description + "||" + lst.RequiredInfo + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";
        //.............................5.1 Technical requirements (optional)..........................................................................
        general_description += "=Technical requirement(optional)=" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Technical requirements (optional)" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Requirements Category !!  Description of category" + "\n";
        general_description += "|-" + "\n";
        foreach (TechnicalRequirement lst in dtReq)
        {
            general_description += "|" + lst.CategoryReq + "||" + lst.CategoryDescription + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "! Requirement ID (R-ID) !!  Description of Requirement" + "\n";
        general_description += "|-" + "\n";
        foreach (TechnicalRequirementSecondPart lst in dtReqSecond)
        {
            general_description += "|" + lst.RID + "||" + lst.RequirementDescription + "\n";
            general_description += "|-" + "\n";
        }
        general_description += "|}" + "\n";
        //.............................6.1 Terms and definitions..........................................................................
        general_description += "=Terms and definitions=" + "\n";
        general_description += "{| class=\"wikitable\"" + "\n";
        general_description += "|-" + "\n";
        general_description += "! Common terms and definitions" + "\n";
        general_description += "|-" + "\n";
        general_description += "! concept !! Definition" + "\n";
        general_description += "|-" + "\n";
        foreach (TermsAndDefinitions lst in dtTerms)
        {
            general_description += "|" + lst.Concept + "||" + lst.Definitions + "\n";
            general_description += "|-" + "\n";
        }

        general_description += "|}" + "\n";
        this.wikitext = general_description;
    }
}