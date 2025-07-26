using OutScribed.Domain.Exceptions;
using OutScribed.Application.Repositories;
using MediatR;
using OutScribed.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OutScribed.Domain.Models.TalesManagement.Entities;
using System.ComponentModel.Design;

namespace OutScribed.Application.Features.TalesManagement.Commands.CreateTaleComment
{
    public class CreateTaleCommentCommandHandler(IUnitOfWork unitOfWork, IErrorLogger errorLogger)
        : IRequestHandler<CreateTaleCommentCommand, Result<CreateTaleCommentResponse>>
    {

        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IErrorLogger _errorLogger = errorLogger;
       
        public async Task<Result<CreateTaleCommentResponse>> Handle(CreateTaleCommentCommand request, CancellationToken cancellationToken)
        {

            //Validate request
            var validator = new CreateTaleCommentCommandValidator();
            
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            
            if (validatorResult is not null && validatorResult.IsValid == false)
            {
                var errors = string.Join(". ", (validatorResult.Errors.Select(x => x.ErrorMessage).ToList()));

                var errorResponse = new Error(Code: StatusCodes.Status500InternalServerError,
                              Title: "Validation Errors",
                              Description: $"The following errors occured: '{errors}'.");

                _errorLogger.LogError(errorResponse.Description);

                return errorResponse;
            }

            Tale? tale = await _unitOfWork.TaleRepository.GetTaleById(request.TaleId);

            //Checks if tale exists
            if (tale is null)
            {
                var errorResponse = new Error(Code: StatusCodes.Status404NotFound,
                                              Title: "Tale Not Found",
                                              Description: $"Tale with Id: '{request.TaleId}' was not found.");

                _errorLogger.LogError($"Tale with Id: '{request.TaleId}' was not found.");

                return errorResponse;
            }

            //save comment
            var result = tale.SaveComment(
                request.Details,
                request.AccountId,
                await _unitOfWork.UserRepository.GetUsernameById(request.AccountId));

            if (result.Error is not null)
            {
                _errorLogger.LogError(result.Error.Description);

                return result.Error;
            }

            //Add user to repository
            _unitOfWork.RepositoryFactory<Tale>().Update(tale);

            //await CreateSeedComments();

            //Save changes
            try
            {
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                //Return success
                return new CreateTaleCommentResponse
                {
                    Comment = await _unitOfWork.TaleRepository.LoadTaleComment(request.AccountId, result.Value)
                };

                //return new CreateTaleCommentResponse();

            }
            catch (Exception ex)
            {

                 _errorLogger.LogError(ex);

                return new Error(Code: StatusCodes.Status500InternalServerError,
                                              Title: "Database Error",
                                              Description: ex.Message);
            }
        
        }

        private async Task CreateSeedComments()
        {

            List<string> commentators = new()
{
    "09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "45637970-409d-49f6-8c28-a012b5989666",
    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
    "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
    "edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
};

            List<string> commentators1Layer = new()
{
    "09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "45637970-409d-49f6-8c28-a012b5989666",
    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
    "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
    "edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
"09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "45637970-409d-49f6-8c28-a012b5989666",
    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
    "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
    "edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
"09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "45637970-409d-49f6-8c28-a012b5989666",
    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
    "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
    "edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
};

            List<string> commentators2Layer = new()
{

    "45637970-409d-49f6-8c28-a012b5989666",
    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
"09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
"edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
    "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
  "45637970-409d-49f6-8c28-a012b5989666",
    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
"09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
"edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
    "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
  "45637970-409d-49f6-8c28-a012b5989666",
    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
"09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
"edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
    "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",


};

            List<string> commentators3Layer = new()
{


    "4d1f57b6-b0f8-428d-a048-2386ce29e139",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
 "09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "45637970-409d-49f6-8c28-a012b5989666",
 "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
    "edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
 "4d1f57b6-b0f8-428d-a048-2386ce29e139",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
 "09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "45637970-409d-49f6-8c28-a012b5989666",
 "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
    "edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",
 "4d1f57b6-b0f8-428d-a048-2386ce29e139",
    "786ecd05-4f98-42d6-a60f-1b97de7a453a",
 "09f72cdb-e18c-429f-b0dc-9fdddd236f52",
    "28ca87a5-4e7b-4fc7-a6a0-eb1a60ce2e25",
    "45637970-409d-49f6-8c28-a012b5989666",
 "cdffd556-b333-4c73-8d7e-d9b2c60db9fb",
    "edda6130-7527-400b-aab3-b5bec9d47f73",
    "fdf844dc-806a-4caa-93d6-e625452985a6",
    "978ea89d-127b-4fac-925a-4baaf4d811ca",
    "a72a08d0-a47a-47e6-80d2-4455dabc845d",

};



            //1 - attack of the paper tigers

            List<string> tigers_1 = new()
{
    "This satire is razor-sharp! The paper tiger metaphor brilliantly captures how imagined dangers can become real when fear takes over. AI panic often feels like self-fulfilling prophecy",

"The idea that AI engineers might sabotage their own work to gain recognition is bold! While unsettling, it does raise a valid point—how often do we see creators being ignored until disaster strikes?",

"This was hilarious! The imagery of a child being tricked by his own creation is so apt for the way AI developers are hyping dangers they helped build. Brilliantly executed satire.",

"The critique of AI alarmism is refreshing! While I think AI does pose risks, this article highlights how ego can drive fear narratives just as much as genuine concern.",

"What a clever take on human psychology! The little boy and paper tiger reflect how sometimes we manufacture fears, and then those fears begin to control us. AI engineers should take note!",

"While this is a fun piece, I think it oversimplifies AI concerns. AI harms aren’t just imagined—they’re real, and ethical oversight is needed. But I see the satire’s point: fear-mongering can be self-serving.",

"This article makes me wonder—how much do AI developers actually believe in the dangers they warn us about? Are they genuinely concerned or just seeking more influence?",

"The symbolism in this satire is spot on! We often give life to our own fears, exaggerating threats for attention. But AI’s dangers can’t be dismissed so easily—it’s more complex than just egos.",

"AI developers are modern-day Frankensteins! They create something groundbreaking, then sound the alarm when it starts behaving unpredictably. This satire calls that out in an amusing way.",

"I appreciate the wit here, but let’s not downplay AI risks entirely. AI bias, automation-driven unemployment, deepfakes—these are real concerns. But yes, fear should be proportionate, not theatrical.",

};

            List<string> tigers_2 = new()
{

"This satire is biting! AI developers often warn about their creations going rogue, yet they keep pushing boundaries. Are they protecting us or just setting the stage for their own hero moment?",

"Fear is powerful, and this article showcases that brilliantly. The paper tiger transforms into something truly menacing—not because it was dangerous, but because of perception. A perfect AI parallel!",

"This reads like a psychological experiment. If you convince people something is a threat long enough, it starts to feel real. The AI panic feels like that—self-created, then self-feeding.",

"The little boy’s trick backfiring is a perfect metaphor for AI developers warning of dangers they helped create. The satire is sharp, but it makes me wonder—are they actually right?",

"AI developers deserve recognition for their work, but this satire suggests that fear might be their way of demanding attention. If true, that’s troubling, and this satire captures it perfectly.",

"The paper tiger works so well as a metaphor! Something harmless can be manipulated into seeming dangerous—and AI’s reputation often teeters between miraculous innovation and looming threat.",

"This article highlights an interesting dilemma: AI developers are both creators and critics of their own work. Are they genuinely worried, or is it just a way to influence public perception?",

"A thought-provoking take! The satire pushes the idea that AI concerns are exaggerated, but what if that exaggeration is justified? It makes me question whether we’re dismissing real risks too easily.",

"Satirical, yes—but it mirrors reality. Fear of AI is often more about power and control than actual risks. Those who build AI want to decide the conversation, and this piece calls them out.",

"The humor in this is subtle yet effective. The idea of a child being fooled by his own prank is genius, and it reflects how AI developers might be caught in the fear narrative they started.",

"AI developers might just be the boy in this story—playing tricks until reality hits back. A compelling critique of self-inflicted fear, though I still wonder: what if AI turns out to be truly dangerous?",

"A really clever analogy. People create their own monsters, then panic when they seem real. Fear-mongering in AI discussions can sometimes do more harm than good.",

"The satire cuts deep! AI developers sound alarms, but they also push progress. This makes me wonder—do they really fear AI, or do they just want control over its narrative?",

"The article cleverly shows how perception creates fear. AI engineers may be pushing warnings just to shape the industry, but their concerns can’t be dismissed completely.",

"Brilliant piece! The paper tiger is a great symbol for irrational fears and misplaced panic. AI development needs scrutiny, but fear shouldn't be the main driver of discussions.",

"What if the boy hadn’t panicked? The whole story would be different. The same applies to AI—our response to perceived threats determines outcomes, sometimes more than the actual technology.",

"The satire works because it’s both humorous and unsettling. AI engineers raising alarms may seem self-serving, but the fears they highlight could still be valid.",

"The message here is clear—fear, once established, becomes hard to control. A timely reminder that AI discourse should be thoughtful, not reactive.",

"Society needs balance—we shouldn’t be reckless, but we also shouldn’t fear innovation.",

"The irony is strong in this satire! AI experts create revolutionary technology, then shout warnings about its potential dangers. Are they truly concerned or just ensuring they remain relevant?",

"Fear can be a powerful tool. This article highlights how panic often stems from perception rather than reality. AI discussions should be rooted in facts, not just alarmist narratives.",

"The boy in this story mirrors AI developers—a creator who suddenly sees his work as a threat. This satire makes me wonder: do AI engineers genuinely fear AI, or is it all part of a grander strategy?",

"A fun read! The paper tiger metaphor brilliantly captures how threats can be exaggerated. AI discourse needs balance—neither full dismissal nor complete hysteria.",

"This satire makes me reconsider AI alarmism. Some concerns are legitimate, but others feel sensationalized. The industry needs transparency, not fear-mongering.",

"A thought-provoking piece! If AI developers were ignored when raising valid concerns, would they resort to making AI truly dangerous just for attention? This satire plays with that unsettling idea.",

"I love the hidden layers in this piece! It highlights how the way we react to perceived threats can shape reality. AI’s risks should be weighed carefully—neither ignored nor exaggerated.",

"This satire is funny but also unsettling. AI engineers might not want disaster, but they sure want recognition. Do they fear their own creations, or do they just want to control the conversation?",

"A brilliant metaphor! The boy uses the paper tiger to manipulate his sister, just like AI fears can be used to shape narratives. AI developers need scrutiny, but so do their warnings.",

"This piece raises tough questions: Are AI warnings genuine? Or are they just self-serving? This satire is sharp and timely—AI discussions often thrive on fear.",

"The satire makes an important point—public perception can be easily swayed. Are AI engineers guiding us toward responsible caution, or playing a role in crafting hysteria?",
};

            List<string> tigers_3 = new()
{
"I loved this article! It cleverly critiques how industries can control public narratives. Fear of AI may not always be genuine—it could also be strategic.",

"A fantastic read! Fear drives attention, and AI warnings sometimes feel less about ethics and more about influence. This satire nails the hypocrisy.",

"The story is deceptively simple, yet it’s packed with meaning. AI’s risks aren’t just about the technology—it’s about the people behind it, their ambitions, and their control over fear narratives.",

"It’s fascinating how fear can evolve from something harmless. AI engineers may have started with legitimate concerns, but now it feels like a competitive tool rather than a genuine warning.",

"This satire explores something crucial—the relationship between AI developers and their creations. Should we trust their fears, or are they exaggerating for their own benefit?",

"AI discussions have become a mix of genuine concern and strategic panic. This satire pushes that idea brilliantly—sometimes those warning us also have the most to gain from fear.",

"The boy’s paper tiger was harmless until fear took over. AI conversations often follow the same pattern—what starts as caution escalates into outright hysteria.",

"If AI developers controlled their own fear narratives, would AI panic look different? This satire raises the question of who truly benefits from these conversations.",

"This article was fascinating! It felt playful, yet it addressed a serious issue: how fear is shaped by those with power. AI warnings need scrutiny just like AI itself does.",

"The balance between concern and manipulation is well illustrated here. AI engineers might want recognition, but should we dismiss their fears entirely? The satire makes it difficult to say.",

"The satire gets to the heart of it—AI’s dangers might be real, but who’s framing the conversation? The power dynamics between creators and critics make for an interesting debate.",

"It’s refreshing to see someone challenge AI fear-mongering. AI has risks, but blowing them out of proportion serves no one. Well-crafted satire!",

"This piece subtly questions whether AI panic is self-inflicted. If developers shaped AI’s dangers, are they now shaping AI’s fear too? Brilliantly written satire.",

"The story is funny, but it holds a serious implication—when does a warning turn into manipulation? AI discussions need transparency, not sensationalism.",

"This satire cleverly highlights how recognition drives fear narratives. AI developers may genuinely worry, but some warnings feel more about influence than safety.",

"A well-structured piece! Fear is a tool, and this satire makes that point clear. AI discussions must be honest—fear shouldn’t be a marketing strategy.",

"The humor in this piece makes it effective! The little boy’s prank turning into real fear mirrors AI debates—a small concern can spiral into mass panic if unchecked.",

"This satire hits hard! AI fears are often wrapped in ego and industry control. Are AI engineers warning us, or ensuring they stay relevant? Great commentary!",

"A playful read with serious undertones! Technology should be analyzed carefully, but fear shouldn’t be exaggerated for influence. Thoughtful and well-written satire.",

"The metaphor of the paper tiger is perfect! AI dangers may be real, but should we let fear dictate progress? This article offers a fresh perspective.",

"This satire captures the essence of fear dynamics perfectly! Sometimes those warning about danger are the ones with the most to gain from it. AI discussions should be based on reason, not self-serving narratives.",

"The paper tiger is a brilliant symbol! AI engineers raising alarms might not be entirely wrong, but this satire makes us question whether fear is being amplified for personal or industry benefit.",

"This piece is sharp and timely. The AI industry is fueled by hype—sometimes about innovation, sometimes about fear. Who controls the narrative decides how we perceive AI.",

"A well-crafted satire! Fear often has a life of its own, much like the paper tiger in this story. AI warnings must be justified, not dramatized for attention.",

"The balance of humor and critique in this article is masterful! AI developers sometimes play both the hero and the alarmist—this satire exposes that duality brilliantly.",

"I enjoyed this read! AI’s risks exist, but do developers frame them to shape public perception? This satire questions how much of the AI fear is genuine and how much is strategic.",

"AI engineers must walk a fine line. If they hype AI’s power too much, people get scared. If they dismiss its dangers, they look irresponsible. This satire highlights that dilemma well.",

"The symbolism here is fantastic! The boy tricks his sister, but in the end, he becomes the one who’s scared. AI developers should reflect on how their own warnings shape public fear.",

"The most powerful part of this satire is how fear builds from something harmless. If AI experts overstate risks, does it make them real? A thought-provoking read!",

"A clever and humorous take on the AI debate! Fear is an effective tool, but when used irresponsibly, it distorts reality. This satire brings attention to that issue brilliantly.",
};

            List<string> tigers_4 = new()
{
"A striking metaphor—the paper tiger started as a joke but became terrifying. AI concerns follow this pattern—sometimes the more we discuss them, the more real they feel.",

"This satire is an eye-opener! AI engineers want recognition for their innovations, but controlling the fear narrative gives them influence too. A compelling critique.",

"The humor in this piece makes it accessible, but the meaning runs deep—AI warnings must be honest, not exaggerated for public manipulation. Great writing!",

"It’s fascinating how fear spreads—this satire uses a simple metaphor to expose a complex issue. Are AI concerns truly about ethics, or is there a deeper motive behind them?",

"The article cleverly questions how AI discourse is shaped. Some fears are justified, but some seem manufactured. Are we seeing genuine warnings, or just strategic influence?",

"A playful yet insightful read! The AI discussion is too often dominated by fear tactics. This satire offers a fresh take on how those warnings might be self-serving.",

"This satire nails it—the line between caution and fear-mongering is thin. AI developers need scrutiny, but so do the motives behind their warnings.",

"An engaging read! Fear narratives can be powerful tools. AI engineers may be warning us, but this satire makes us ask: are they shaping fear to their advantage?",

"The brilliance of this satire is how it simplifies a complex issue. Fear spreads quickly, and once people believe AI is dangerous, that belief becomes hard to challenge.",

"A sharp and entertaining critique! The AI industry thrives on attention, and sometimes fear is the easiest way to get it. This satire exposes that angle effectively.",

"A must-read satire! The paper tiger is the perfect representation of exaggerated fears—sometimes danger is less about reality and more about perception.",

"This piece highlights how narratives shape industries. AI engineers raising alarms might be right, but what if they’re also driving fear for their own benefit?",

"The humor here is subtle but powerful—AI fear doesn’t come from the technology itself, but from how it’s framed. This satire brilliantly questions who controls that framing.",

"Fear often grows beyond its origins—this story illustrates how something imaginary can become terrifying. AI discussions must be rooted in facts, not just panic.",

"A brilliant critique of fear manipulation! AI warnings must be justified, not exaggerated. This satire makes us rethink whether we’re reacting to real concerns or manufactured narratives.",

"A compelling read! AI engineers often play the role of both creator and critic—this satire examines whether fear narratives are as calculated as their technology.",

"I loved this article! The balance of humor and critique is perfect. AI’s dangers must be taken seriously, but fear tactics shouldn’t dominate discussions.",

"A fascinating take on power and perception! AI warnings shape how people react to the technology—this satire makes us question whether fear is being used as a tool.",

"The paper tiger story is a perfect analogy—fear often develops from perception, not reality. AI conversations need clarity, not exaggerated concerns.",

"A sharp satire! AI engineers wield immense influence, and fear is sometimes their most effective tool. This article makes us think about how much of AI panic is real.",

"AI developers must balance progress with responsibility. This satire highlights how fear can be both a warning and a weapon. A refreshing perspective!",

"An insightful read! The idea that fear is self-created in the AI industry is powerful. Are we discussing AI responsibly, or are we fueling panic unnecessarily?",

"Brilliant commentary! AI developers must acknowledge real risks, but hyping fear for control is troubling. This satire cleverly explores that dynamic.",

"The humor makes this article stand out! AI engineers have legitimate concerns, but are they also shaping narratives for influence? A great discussion starter.",

"A fresh take on AI fear-mongering! Sometimes industries thrive on attention, and fear is a guaranteed way to get it. This satire calls out that strategy.",

"A thought-provoking satire! It raises an important question—who benefits from AI panic? AI developers, policymakers, or the general public?",

"Fear as a tool is explored brilliantly here! AI risks are real, but some narratives feel less about ethics and more about control. A fantastic read!",

"The satire’s brilliance lies in how it simplifies fear dynamics. AI engineers may warn us with good intentions, but when does caution turn into manipulation?",

"A sharp and clever critique! Fear often takes on a life of its own—this satire shows how AI discourse might be shaped more by perception than reality.",

"A powerful closing thought—the paper tiger became terrifying through illusion. AI fears can be real, but they must be weighed against facts, not just narratives.",
};

            Tale? tale_tiger = await _unitOfWork.TaleRepository.GetTaleById(Guid.Parse("caf2c130-7619-4b5d-bcfb-081ed59c4429"));

            if (tale_tiger is not null)
            {
                //create the comments
                for (var i = 0; i < tigers_2.Count; i++)
                {

                    var resulti = tale_tiger.SaveComment(
                        tigers_2[i], 
                        Guid.Parse(commentators1Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
                        );

                    //for the first comment in the list
                    if (i == 0)
                    {
                        for (var j = 0; j < tigers_1.Count; j++)
                        {
                            tale_tiger.SaveResponse(
                                resulti.Value, 
                                tigers_1[j], 
                                Guid.Parse(commentators[j]),
                                Guid.Parse(commentators1Layer[i]),
                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
                        }
                    }

                    var resultk = tale_tiger.SaveResponse(
                        resulti.Value, 
                        tigers_3[i], 
                        Guid.Parse(commentators2Layer[i]),
                         Guid.Parse(commentators1Layer[i]),
                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

                    var resultl = tale_tiger.SaveResponse(
                        resultk.Value, 
                        tigers_4[i], 
                        Guid.Parse(commentators3Layer[i]),
                        Guid.Parse(commentators2Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

                }

                _unitOfWork.RepositoryFactory<Tale>().Update(tale_tiger);

            }



            //2 - art of the deal

            List<string> deals_1 = new()
{
    "This satire exposes the harsh reality—judicial independence is often compromised by political agendas! The deal-making over Supreme Court seats is unsettling",

"A brilliantly crafted piece! The idea that court-packing discussions could lead to constitutional reshaping is a sharp critique of how political power trumps legal integrity",

"The president’s regret over his appointee is both hilarious and revealing! This satire showcases how judicial appointments often prioritize ideology over actual jurisprudence",

"A powerful commentary on the erosion of judicial independence! When justices are evaluated based on politics rather than legal expertise, democracy suffers",

"The satire cleverly illustrates how power struggles overshadow legal principles! The impeachment of a justice for perceived ideological betrayal is a chilling example of political purges",

"A must-read satire! It explores how political parties will manipulate institutions to serve their interests, regardless of constitutional norms",

"The conservative justice’s speech is the perfect reflection of judicial independence—yet it is ignored, proving political loyalty matters more than the law",

"This satire paints an unsettling picture—judicial appointments have become mere chess pieces, moved strategically to shape outcomes rather than uphold justice",

"A sharp critique of court-packing debates! The story suggests that both parties, at different times, will use institutional leverage to secure ideological advantage",

"The satire captures the absurdity of modern judicial politics! Supreme Court seats should be about legal expertise, not a battleground for political vendettas",
};

            List<string> deals_2 = new()
{
  "A sharp critique of political maneuvering! The president’s willingness to sacrifice a justice for strategic advantage reveals how little judicial independence is respected",

"This satire brilliantly showcases how political power operates—deals and compromises override constitutional principles when it serves partisan interests",

"The balance of power in the court shouldn’t be dictated by political whims! This satire makes an unsettling yet accurate reflection of today’s judicial battles",

"A thought-provoking piece! It highlights the dangerous precedent of removing justices simply because they don’t align with expected ideological positions",

"A must-read satire! It illustrates how political deals often reshape institutions in ways that fundamentally alter governance",

"The president’s negotiation with liberals is a fascinating plot twist! It exposes how political strategy can lead to unexpected alliances",

"The satire raises important concerns—when judicial decisions are seen as betrayals rather than independent rulings, democracy is in trouble",

"This story is an eye-opener! Political purges disguised as legal proceedings are a warning sign for any institution that should be impartial",

"A disturbing but insightful satire! It highlights how judicial appointments and removals are driven by partisanship rather than legal integrity",

"The conservative justice’s impassioned defense is powerful! It forces readers to question whether ideology should ever override fair jurisprudence",

"This satire expertly critiques court-packing discussions! It suggests that constitutional amendments may become tools for political power grabs rather than stability",

"A fascinating take on judicial politics! The president’s regret over his own appointee exposes the dangers of selecting justices purely based on ideology",

"The satire cleverly illustrates how constitutional amendments can be used as bargaining chips rather than thoughtful reforms",

"The conservative justice’s impeachment is a chilling example of judicial purges! It forces readers to reflect on whether courts should ever be reshaped based on political demands",

"A compelling critique of ideological expectations! The justice is accused of betrayal simply for ruling based on law rather than party allegiance",

"The president’s deal-making in this satire mirrors real-world political negotiations! It showcases how power can be leveraged to restructure institutions",

"This satire is both hilarious and unsettling! The idea that justices must remain ideologically pure or risk impeachment is a dangerous precedent",

"A fascinating read! It questions whether long-term stability is ever possible when judicial appointments are made based on political loyalty",

"A bold critique of judicial integrity! The president’s deal reveals how courts are often treated as political tools rather than independent entities",

"A must-read satire! It captures the absurdity of judicial loyalty tests—justices should be interpreting the law, not pledging allegiance to a party",

"The conservative justice’s impeachment trial is a deeply unsettling reflection of political interference in the judiciary! A powerful critique of modern judicial struggles",

"A masterful satire on judicial politics! It forces readers to ask whether courts can ever be truly impartial in a system dominated by ideological expectations",

"A sharp commentary on presidential decision-making! The satire suggests that leaders often regret their choices but find strategic ways to correct them",

"This satire makes a brilliant point—court-packing debates always stem from the same desire: control.  Regardless of the side pushing for changes, the goal is the same",

"A fantastic exploration of judicial politics! The conservative justice’s removal proves that rulings can be punished when they don’t align with party expectations",

"A compelling and timely satire! It highlights how political strategies often determine constitutional reforms, rather than a genuine need for legal stability",

"The satire cleverly critiques the idea of undoing judicial appointments! If judges are expected to rule based on ideology, then independence is meaningless",

"A dark but thought-provoking piece! It exposes how political convenience often dictates judicial restructuring rather than upholding constitutional principles",

"The president’s deal is fascinating! Instead of fighting court-packing efforts, he uses them to negotiate a long-term restructuring—what a clever twist!",

"A deeply unsettling satire! The conservative justice’s impeachment forces readers to ask: should courts serve justice or partisan interests?",
};

            List<string> deals_3 = new()
{
   "A sharp and timely satire! It highlights how judicial appointments are increasingly evaluated through political lenses rather than legal expertise",

"The satire makes an unsettling point—court-packing isn’t about justice, it’s about power.  The president’s negotiations prove that the court is seen as a political tool",

"A brilliant critique of ideological battles within the judiciary! The impeachment of a justice for perceived betrayal is a dark reflection of political interference",

"The satire perfectly captures the tension between judicial integrity and political expectations! Justices should interpret the law, not serve partisan interests",

"A fascinating exploration of presidential decision-making! The idea of undoing an appointment through political negotiation is both clever and deeply unsettling",

"A must-read satire! It exposes the way judicial structures can be reshaped based on temporary political victories rather than long-term stability",

"The conservative justice’s passionate defense is powerful—it forces readers to reflect on whether ideological purity should be expected from justices",

"The satire cleverly illustrates the absurdity of treating courts as political battlegrounds! Judicial decisions should stand on legal reasoning, not party loyalty",

"A compelling critique of judicial independence! The president’s regret over his own nominee reveals how political considerations often outweigh legal qualifications",

"The impeachment of a justice for failing to meet ideological expectations is deeply unsettling—this satire forces reflection on how courts are viewed today",

"A sharp look at political deal-making! The president’s willingness to trade a Supreme Court pick for long-term restructuring is a clever critique of power dynamics",

"This satire brilliantly showcases how Supreme Court appointments are treated like chess pieces—strategically moved to serve ideological agendas",

"A fascinating satire! It raises the question: should judicial appointments be evaluated based on ideology or adherence to the law?",

"The conservative justice’s passionate speech is haunting—she represents judicial independence, yet political forces demand her removal",

"A must-read reflection on institutional integrity! The satire highlights how ideological expectations often dictate judicial careers",

"A powerful critique of political interference in the judiciary! It forces readers to consider whether courts should ever be manipulated for strategic purposes",

"The president’s regret over his own appointee is both hilarious and deeply concerning! The satire reveals how judicial picks often prioritize ideology over law",

"A fascinating twist on court-packing! The idea of codifying a fixed number of justices could ensure stability, but political deals could still shape appointments",

"The satire cleverly illustrates the absurdity of removing justices for failing to adhere to strict ideological loyalty! Judicial decisions should be based on law, not expectations",

"A deeply insightful satire! It forces readers to reflect on whether modern courts truly serve justice or simply reflect partisan power struggles",

"A bold critique of judicial maneuvering! The satire explores how political strategy can completely reshape institutions under the guise of reform",

"The conservative justice’s removal is a chilling reminder that courts are never free from political influence—this satire brilliantly captures that reality",

"A must-read piece! It explores how judicial appointments are rarely about qualifications, but about serving long-term political interests",

"The president’s negotiation with liberals is a fascinating development—it highlights how power plays often override traditional partisan rivalries",

"This satire exposes the reality of modern judicial battles! Political strategy determines who sits on the court, not legal qualifications",

"A dark but deeply relevant satire! The story makes it clear that judicial independence is often compromised by shifting political demands",

"A brilliant exploration of judicial purges! The impeachment of a justice for failing to uphold ideological purity is a warning sign for institutional integrity",

"The satire cleverly showcases how presidential regret over an appointee can lead to political reshuffling rather than long-term judicial stability",

"A fascinating critique! Court-packing discussions highlight how institutions are often manipulated to serve immediate political needs rather than national interests",

"A compelling satire! It highlights the dilemma of judicial independence—should justices rule according to the law or according to party expectations?",
};

            List<string> deals_4 = new()
{
  "A fresh and engaging take on judicial battles! The impeachment of a justice purely for ideological reasons is a troubling example of political manipulation",

"A bold statement on Supreme Court appointments! The satire reveals how judicial selections are rarely about legal expertise, but about long-term strategy",

"The conservative justice’s passionate defense is powerful—it forces readers to question whether judicial rulings should be evaluated based on law or ideology",

"A brilliantly crafted satire! It makes readers think about whether courts should ever be reshaped based on political convenience",

"A fascinating perspective on judicial maneuvering! The president’s regret highlights the ongoing struggle to balance ideology with governance",

"A must-read reflection on judicial independence! The satire forces readers to consider whether courts should be immune to political influence",

"The story makes a compelling case that courts are never truly independent! Political calculations determine judicial appointments, not legal expertise",

"A powerful critique of Supreme Court restructuring! This satire exposes the reality that court-packing debates are often about control rather than fairness",

"The satire brilliantly critiques political deal-making! The president’s negotiation ensures long-term stability but also exposes the dangers of strategic purges",

"A bold satire on judicial expectations! The impeachment of a justice for perceived ideological betrayal is a troubling precedent",

"The president’s deal is a sharp critique of court-packing efforts! It suggests that long-term solutions may be possible but will always involve political maneuvering",

"A compelling satire! It explores how justices are expected to serve political goals rather than uphold constitutional principles",

"A masterful commentary on judicial loyalty! The satire suggests that courts should remain independent, but reality often dictates otherwise",

"The conservative justice’s impeachment is a dark but fascinating twist—it forces readers to question whether institutional purges should ever be justified",

"The satire is both engaging and unsettling! It raises the question of whether judicial decisions should be evaluated based on interpretation of the law or political expectations",

"A powerful critique of judicial restructuring! The president’s deal suggests that constitutional amendments can be strategic tools rather than genuine reforms",

"The satire exposes how court-packing efforts are never about fairness—they are about securing long-term ideological advantage",

"The conservative justice’s impeachment trial is haunting! It suggests that judicial independence is secondary to political loyalty",

"A brilliantly written satire! It highlights the absurdity of judicial maneuvering, forcing readers to question how courts should truly operate",

"A fascinating take on Supreme Court appointments! The president’s regret highlights the difficulties of selecting justices purely based on ideology",

"A thought-provoking satire! The impeachment of a justice for failing to adhere to expected rulings is a powerful reflection of judicial constraints",

"The satire cleverly critiques how institutional restructuring is often driven by political whims rather than constitutional needs",

"A bold and insightful piece! The idea that court-packing could lead to permanent restructuring is a fascinating concept",

"A must-read satire! It explores how political strategies often determine judicial stability rather than legal expertise",

"The conservative justice’s speech is deeply compelling! It forces readers to think about whether courts should ever be reshaped due to ideological expectations",

"A fresh critique of judicial politics! The satire cleverly illustrates how institutions are never truly independent from political maneuvering",

"The president’s negotiations are a fascinating twist! It reveals how strategic compromises often dictate constitutional amendments",

"A brilliantly written satire! The story captures the absurdity of court-packing discussions while offering a compelling reflection on judicial power",

"The satire makes a bold statement—judicial independence is secondary to political expectations in modern governance",

"A compelling critique of political purges! The impeachment of a justice purely for failing to uphold an ideological narrative is a deeply unsettling concept",
};

            Tale? tale_deal = await _unitOfWork.TaleRepository.GetTaleById(Guid.Parse("efcb2fe0-5259-44d9-bcb1-7616e345eec4"));

            if (tale_deal is not null)
            {
                //create the comments
                for (var i = 0; i < deals_2.Count; i++)
                {

                    var resulti = tale_deal.SaveComment(
                        deals_2[i],
                        Guid.Parse(commentators1Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
                        );

                    //for the first comment in the list
                    if (i == 0)
                    {
                        for (var j = 0; j < deals_1.Count; j++)
                        {
                            tale_deal.SaveResponse(
                                resulti.Value,
                                deals_1[j],
                                Guid.Parse(commentators[j]),
                                Guid.Parse(commentators1Layer[i]),
                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
                        }
                    }

                    var resultk = tale_deal.SaveResponse(
                        resulti.Value,
                        deals_3[i],
                        Guid.Parse(commentators2Layer[i]),
                         Guid.Parse(commentators1Layer[i]),
                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

                    var resultl = tale_deal.SaveResponse(
                        resultk.Value,
                        deals_4[i],
                        Guid.Parse(commentators3Layer[i]),
                        Guid.Parse(commentators2Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

                }

                _unitOfWork.RepositoryFactory<Tale>().Update(tale_deal);

            }


            //3 - king of the beasts

            List<string> kings_1 = new()
{
    "This satire is sharp! The lion makes a compelling argument—if intelligence leads to destruction rather than preservation, is it truly a sign of superiority?",

"A fascinating read! The professor’s defense of humanity collapses under the weight of truth. Buildings, cars, and planes mean nothing if they poison the world.",

"The lion’s questioning of human supremacy is a powerful critique. Strength, speed, and skill exist in nature, but mankind’s “greatest gift”—his intelligence—often causes harm.",

"The irony here is brilliant! The professor boasts of man’s ability to shape the world, yet it is that very ability that has led to environmental ruin.",

"A thought-provoking satire! The idea that animals live by necessity while humans kill for pleasure is a chilling indictment of mankind’s history.",

"The debate between the lion and the professor is hilarious yet deeply unsettling. The professor’s arguments crumble when confronted with humanity’s disregard for nature.",

"This satire cleverly exposes mankind’s arrogance! Intelligence should mean wisdom and sustainability, but history shows it often leads to destruction instead.",

"The lion’s reasoning is flawless—nature exists in balance, while humans disrupt it. If intelligence leads to planetary ruin, it is not superiority, but tragedy.",

"A stunning metaphor for the environmental crisis! The lion highlights how every creature plays a role in nature, but man has only sought to dominate it.",

"The satire is biting and timely! If mankind prides itself on intelligence, why does it persist in behaviors that threaten its own survival?",
};

            List<string> kings_2 = new()
{
  "A dark but necessary critique—humanity wages war, pollutes the earth, and destroys ecosystems, yet claims intellectual superiority. The lion exposes the hypocrisy.",

"The professor’s argument is shattered the moment the lion questions humanity’s impact on the environment. Intelligence means little if it leads to destruction.",

"A perfect commentary on human arrogance! The professor assumes superiority, but his arguments collapse under the weight of history’s destruction.",

"The satire makes a strong environmental statement—nature thrives without human intervention, yet man continuously interferes, leaving only chaos.",

"The dialogue between the professor and the lion is brilliant! The professor symbolizes human vanity, while the lion represents the wisdom of nature.",

"A fantastic read! The lion is right—intelligence should be measured by balance and sustainability, not by dominance and destruction.",

"The humor here is sharp, but the message is even sharper—if man were truly superior, he wouldn’t need to justify his existence in the face of nature’s purity.",

"This satire captures the essence of mankind’s failures! The professor’s argument about intelligence crumbles when placed against the damage humanity has caused.",

"The lion’s logic is impeccable! What is superiority if it leads to the destruction of the very world that sustains life?",

"A chilling yet insightful satire! The professor’s defense of mankind is weak, because history shows intelligence has often led to destruction rather than enlightenment.",

"Humanity sees itself as the ruler of nature, but this satire challenges that assumption! If intelligence doesn’t come with responsibility, is it truly an advantage?",

"The lion speaks the truth—humans invent marvels but destroy ecosystems. Nature would thrive without mankind’s interference.",

"A beautifully crafted satire! Intelligence should lead to preservation, yet mankind has repeatedly ignored the balance of nature.",

"The lion's wisdom is unmatched! Strength alone does not make one superior, but if intelligence causes harm, it is not an asset.",

"A thought-provoking satire that forces us to reconsider human impact on the environment. If superiority leads to destruction, is it truly worth celebrating?",

"This story is brilliant! The professor represents humanity’s pride, while the lion embodies the wisdom of nature—an incredible contrast!",

"The satire makes a bold statement! Human intelligence may build civilizations, but it has also led to deforestation, pollution, and destruction.",

"The ending was perfect! The lion pouncing symbolizes the inevitable consequences of mankind’s actions—nature will reclaim what man has damaged.",

"A sharp critique of human arrogance! Man boasts of intelligence yet pollutes the air, poisons the water, and destroys forests—where is the wisdom in that?",

"The lion’s argument is compelling! True superiority should mean balance, yet mankind seems determined to destroy rather than preserve.",

"This satire challenges the very foundation of human superiority! If intelligence leads to destruction, can we truly claim to be the dominant species?",

"The lion’s argument is flawless! Humanity has built towering cities but also poisoned rivers and destroyed forests—is that true progress?",

"The professor’s arrogance crumbles under the weight of reality! Mankind claims dominion over nature yet cannot survive without it—an ironic contradiction.",

"A truly sharp critique! Man views himself as the pinnacle of evolution, yet his actions often reveal recklessness rather than wisdom.",

"The lion is right! Animals live in balance, yet mankind disrupts ecosystems for greed, ambition, and self-indulgence—what a powerful message.",

"The professor's argument falls apart quickly! If intelligence leads to destruction, then perhaps true wisdom lies in preservation, not conquest.",

"This satire exposes the hypocrisy of human superiority! Strength, speed, and cunning exist in nature, but only mankind actively destroys his own home.",

"A beautifully crafted satire! It forces us to reconsider whether intelligence truly makes us superior, or merely reckless in our pursuit of dominance.",

"The comparison between nature and humanity is brilliant! Animals live within limits, yet mankind pushes beyond them, often with disastrous consequences.",

"The lion’s critique of human destruction is deeply insightful! If intelligence were measured by sustainability, animals would be far superior.",
};

            List<string> kings_3 = new()
{
   "This article is both humorous and deeply thought-provoking! The professor’s argument is filled with human arrogance, but the lion exposes the uncomfortable truth.",

"A masterpiece in satire! The story cleverly juxtaposes humanity’s destructive tendencies with nature’s harmony—forcing us to rethink what makes a species truly 'great.'",

"The lion proves an important point—nature flourishes without man, yet mankind struggles without nature. Who really holds power in the grand scheme?",

"The satire holds a mirror to human arrogance! If intelligence were measured by coexistence rather than dominance, mankind would certainly fall short.",

"The lion’s comparison between human actions and natural balance is profound! No species has done more harm to the earth than mankind—an eye-opening critique.",

"The idea that animals kill only for survival while humans kill for pleasure is a powerful statement on morality! This satire forces deep reflection.",

"This satire brilliantly showcases mankind’s contradictions! We claim superiority yet destroy the very world that sustains us—how ironic!",

"A deeply insightful read! The lion exposes the uncomfortable truth—humanity’s intelligence has caused irreparable harm rather than harmony.",

"The professor’s crumbling argument is hilarious but also tragic! When faced with the truth about humanity’s impact, his defense collapses instantly.",

"A stunning metaphor for mankind’s failures! We conquer land, sky, and sea, but at what cost? This satire raises the uncomfortable but necessary question.",

"Humanity’s obsession with superiority blinds it to the destruction it causes—this satire is a brilliant wake-up call!",

"The lion’s reasoning is far more logical than the professor’s! Nature sustains itself, yet mankind seems determined to disrupt that balance.",

"This satire feels painfully relevant! As climate crises unfold, mankind's claim of intelligence seems more questionable than ever.",

"A beautifully structured satire! The lion symbolizes nature’s wisdom, while the professor embodies humanity’s reckless arrogance.",

"The professor’s attempted justification of humanity’s dominance falls apart instantly—there’s no defending destruction disguised as progress.",

"A sharp critique of mankind’s priorities! Intelligence should lead to preservation, yet history proves otherwise—what a tragic irony.",

"The lion’s final pounce is a perfect metaphor for nature reclaiming its place—mankind’s recklessness cannot go unchallenged forever.",

"A magnificent piece! The contrast between natural survival and human exploitation is stark—this satire is an uncomfortable but necessary reflection.",

"Humanity’s tendency to kill for power, greed, and pleasure makes this satire especially biting—intelligence should be an asset, not a weapon.",

"The lion’s wisdom shines throughout the story! Nature’s balance is maintained without intervention, yet mankind continuously disrupts it.",

"The satire raises an excellent point—who truly deserves the title “king of the beasts” when mankind’s actions endanger all life?",

"The professor’s fear at the end is a striking moment! When stripped of his perceived superiority, all that remains is vulnerability.",

"The contrast between nature’s laws and mankind’s laws is brilliantly portrayed! Humanity creates laws yet often ignores them for personal gain.",

"A powerful critique of mankind’s environmental impact! Intelligence should mean responsibility, yet humanity continues to exploit nature for greed.",

"The lion’s final pounce is a symbolic reckoning—nature striking back against its greatest threat: human destruction.",

"A clever and unsettling satire! It forces us to question whether intelligence is truly the measure of superiority or merely a tool for reckless ambition.",

"The lion’s argument is hauntingly accurate! No species has damaged the earth more than mankind—can we truly claim to be nature’s ruler?",

"This article is both darkly humorous and brutally honest! It showcases how mankind’s pride in intelligence often leads to disastrous consequences.",

"Humanity has reshaped the world, but not always for the better—this satire perfectly captures the weight of that contradiction.",

"The professor’s fear at the end is powerful—without his grand illusions, he is just another creature trying to survive in nature’s unforgiving world.",
};

            List<string> kings_4 = new()
{
  "The satire paints a clear picture—mankind’s actions threaten not only himself but all life on earth. Intelligence without wisdom is a dangerous thing.",

"The lion’s closing argument is devastating! True superiority should mean harmony with nature, yet mankind has prioritized domination over balance.",

"A thought-provoking piece! If intelligence does not lead to sustainability, can it truly be considered an advantage?",

"Humanity takes pride in its technological marvels, yet the lion exposes the truth—these inventions often serve destruction rather than progress.",

"A masterpiece in environmental satire! It strips away mankind’s illusions of dominance and reveals the reality of reckless ambition.",

"The lion’s critique of human warfare is chilling! No other species wages war for ideology—only mankind destroys for reasons beyond survival.",

"This satire brilliantly questions mankind’s claim to superiority! Nature thrives without human interference, yet we continuously disrupt it.",

"A brilliantly woven argument against human arrogance! If intelligence causes destruction rather than preservation, is it truly valuable?",

"The lion’s final strike is poetic justice! When man fails to justify his own actions, he becomes just another vulnerable animal in nature’s grand design.",

"A deeply insightful read! It highlights the true cost of human innovation—progress at the expense of nature is not truly progress.",

"The satire exposes a painful truth—humanity measures success by dominance, yet nature thrives through balance.",

"A spectacular critique of mankind’s environmental failures! Intelligence should mean wisdom, yet history proves otherwise.",

"A striking commentary on human greed! The lion’s argument exposes how humanity prioritizes control over coexistence.",

"The professor’s desperation is perfectly captured! When faced with his own contradictions, he loses the ability to defend mankind’s recklessness.",

"This satire is brilliant! It forces us to reconsider what intelligence really means—if it leads to destruction, is it truly superiority?",

"The lion’s observations are painfully accurate! Every animal plays a role in nature’s cycle, yet mankind insists on disrupting it.",

"A fantastic read! It highlights the irony of human progress—creating marvels while simultaneously destroying the environment.",

"The professor’s failure to justify human superiority is a powerful moment—history does not favor reckless ambition over sustainability.",

"A necessary critique of human arrogance! Nature exists in harmony, yet mankind thrives on disrupting that balance.",

"This satire is both darkly humorous and deeply unsettling—if intelligence cannot coexist with nature, then what is it truly worth?",

"The lion proves an undeniable truth—every animal plays a role in nature except mankind, who continuously distorts it.",

"This article raises a crucial question—if intelligence leads to environmental collapse, is it truly something to be proud of?",

"A powerful message! Mankind’s pride in innovation often overshadows the environmental destruction left in its wake.",

"The satire cleverly exposes human contradictions! We boast about intelligence yet fail to use it wisely—nature, in contrast, thrives effortlessly.",

"The professor’s argument is flawed from the start! Superiority should be measured by sustainability, not unchecked destruction.",

"The lion’s argument is piercing! Intelligence is meant to enrich, yet mankind has used it to exploit—what a tragic contrast.",

"The ending is brilliant! When stripped of illusions, mankind is just another animal struggling to justify its place in nature.",

"A striking satire on humanity’s environmental impact! If intelligence leads to reckless destruction, can we truly call it a gift?",

"A bold and insightful satire! It forces us to confront the uncomfortable truth—mankind’s inventions often come at a great cost.",

"A powerful final statement—nature will endure, but mankind must decide whether intelligence is a force for creation or destruction."
};

            Tale? tale_king = await _unitOfWork.TaleRepository.GetTaleById(Guid.Parse("31b165ec-5fce-4a01-9982-85fd734b5a11"));

            if (tale_king is not null)
            {
                //create the comments
                for (var i = 0; i < kings_2.Count; i++)
                {

                    var resulti = tale_king.SaveComment(
                        kings_2[i],
                        Guid.Parse(commentators1Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
                        );

                    //for the first comment in the list
                    if (i == 0)
                    {
                        for (var j = 0; j < kings_1.Count; j++)
                        {
                            tale_king.SaveResponse(
                                resulti.Value,
                                kings_1[j],
                                Guid.Parse(commentators[j]),
                                Guid.Parse(commentators1Layer[i]),
                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
                        }
                    }

                    var resultk = tale_king.SaveResponse(
                        resulti.Value,
                        kings_3[i],
                        Guid.Parse(commentators2Layer[i]),
                         Guid.Parse(commentators1Layer[i]),
                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

                    var resultl = tale_king.SaveResponse(
                        resultk.Value,
                        kings_4[i],
                        Guid.Parse(commentators3Layer[i]),
                        Guid.Parse(commentators2Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

                }

                _unitOfWork.RepositoryFactory<Tale>().Update(tale_king);

            }


            //4 - overlords

            List<string> overlords_1 = new()
{
    "This satire is terrifyingly realistic! The transition from forced slavery to economic control mirrors how colonial powers have maintained dominance over Africa for centuries.",

"A powerful read! It highlights how systemic poverty was designed to keep African nations dependent, forcing people into migration that benefits the West.",

"The Overlords’ plan is disturbingly clever—they shift control from chains to economic manipulation. This satire exposes how slavery evolves rather than disappears.",

"A thought-provoking critique of colonial strategy! Ending slavery wasn’t about liberation—it was about finding a new way to ensure Africa remained under control.",

"The satire cuts deep! It shows how wealth hoarding and corruption were encouraged to keep African nations weak, leading to self-perpetuating cycles of poverty.",

"Brilliant storytelling! This secret meeting feels all too real, as economic control has been just as effective as direct slavery in keeping Africa in Western hands.",

"The historical accuracy of this satire is striking! Slavery didn't truly end—it simply changed form, with economic dependency replacing physical chains.",

"A chilling reminder that oppression can be disguised as progress. The Overlords’ strategy ensured migration became a form of voluntary servitude.",

"The dialogue is haunting! The idea that slavery continues through poverty rather than force is an uncomfortable truth many refuse to acknowledge.",

"A well-crafted satire that reveals how Western nations still benefit from Africa’s suffering. Systemic poverty ensures the cycle of control continues.",
};

            List<string> overlords_2 = new()
{
  "This satire brilliantly exposes how colonial powers shifted from physical slavery to economic bondage. Independence meant little when financial control remained in Western hands.",

"A chilling revelation! The Overlords knew that ending slavery officially wouldn’t weaken their grip on Africa—poverty would do the job just as effectively.",

"The strategy outlined in this satire is disturbingly familiar. Creating corrupt leaders ensured African nations remained unstable, keeping them dependent on Western influence.",

"The Overlords’ scheme was diabolical! Poverty became the new chain, forcing migration as the only escape—an engineered cycle of servitude.",

"This satire reflects reality all too well! Slavery may have ended on paper, but economic exploitation continues, ensuring Africa remains a source of labor for the West.",

"A masterful critique of Western domination! Controlling Africa through financial corruption was just a continuation of exploitation under a different name.",

"The satire highlights a painful truth—Africa wasn’t freed, it was restructured to ensure it remained under Western control.",

"The dialogue in this piece is haunting! The Overlords understood that controlling wealth and opportunity would keep Africa subjugated without the need for chains.",

"A must-read satire! It exposes how African leaders were encouraged to betray their own people, leaving them impoverished and desperate to migrate.",

"The most unsettling part of this satire is how recognizable the tactics are—this is not just history, but an ongoing reality.",

"The satire exposes the lie of abolition! Slavery ended on the surface, but economic chains ensured Africa remained a tool for Western interests.",

"A deeply thought-provoking story! It forces us to question whether Africa was ever truly freed, or if control simply shifted to financial manipulation.",

"The brilliance of this satire is how it breaks down systemic oppression. Corrupt African leaders ensured their own people remained weak, keeping Western nations in control.",

"A terrifyingly insightful read! The Overlords understood that African poverty would ensure a continuous flow of laborers seeking escape—modern slavery in disguise.",

"This piece is bold and necessary! It exposes how economic dependence was engineered, ensuring Africa remained useful to the West without open slavery.",

"A satirical masterpiece! The idea that forced slavery was merely replaced with economic slavery is a devastating critique of historical exploitation.",

"This satire resonates deeply! African migration is often painted as voluntary, but this article exposes the root causes—systemic impoverishment.",

"The idea that the Overlords planned the end of slavery themselves is chilling! It proves abolition wasn’t about morality—it was about control.",

"A powerful critique! Slavery did not disappear, it simply became voluntary through financial desperation—what a disturbing truth.",

"A necessary satire that forces readers to confront uncomfortable realities! Western influence didn’t end with slavery, it simply evolved.",

"A deeply unsettling read! Slavery continued in the form of poverty, forcing migration rather than physical captivity—a brilliant exposure of history’s deception.",

"The satire’s premise is horrifyingly plausible! The transition from forced labor to economic coercion ensured Africa remained dependent and exploited.",

"A masterful critique of colonial strategy! The Overlords understood that wealth and opportunity control people just as effectively as chains.",

"The discussion in this satire is chilling—it shows how abolition was carefully managed to ensure Africa remained under Western dominance.",

"This satire cleverly highlights how corruption was encouraged! Keeping African leaders greedy ensured Western nations would always maintain leverage.",

"A painful but necessary piece! The illusion of freedom is shattered when one realizes that economic chains replaced physical ones.",

"A harrowing truth! Western nations ensured Africa would remain impoverished, keeping migration as the only escape—slavery in disguise.",

"The dialogue between the Overlords is deeply unsettling! Their plan ensured Africa remained weak, guaranteeing Western supremacy for generations.",

"A brilliant satire! It captures the reality that slavery never truly ended—it evolved into economic subjugation.",

"A must-read! The Overlords’ strategy echoes throughout history, proving that true freedom for Africa was never the West’s intention.",
};

            List<string> overlords_3 = new()
{
   "This satire exposes the manipulation behind abolition! Freedom was just a word, but the system remained designed to ensure Africa’s dependency.",

"The Overlords' plan is chillingly effective! Slavery was never abolished—it was simply repackaged as financial desperation.",

"A powerful and deeply relevant satire! The Western world ensured Africa remained poor, forcing migration as the new form of servitude.",

"A tragic but necessary commentary! It forces readers to question whether abolition was ever truly meant to free Africa, or just to restructure control.",

"A brilliant satire! It unveils how colonial powers designed poverty to ensure Africans would continue working for Western economies.",

"The idea that the Overlords planned slavery’s end to benefit themselves is horrifying! It proves Western nations never intended real equality.",

"A must-read critique of historical oppression! The Overlords engineered economic instability, ensuring Africa would never break free.",

"The conversation between the Overlords is eerily realistic! Manipulating African wealth was the perfect way to maintain control without direct rule.",

"A strikingly relevant satire! Today’s economic struggles trace back to strategies like the one outlined in this piece—modern slavery in disguise.",

"A dark and necessary read! It uncovers the hidden truth that financial control replaced physical chains, ensuring Africa’s continued servitude.",

"A powerful critique of colonial tactics! Slavery was never truly abolished—it was rebranded through economic manipulation.",

"The satire exposes the hidden truth—freedom without financial independence is meaningless, and the Overlords knew it.",

"A brilliantly unsettling read! It shows how Western powers ensured Africa remained vulnerable even after abolition.",

"A harrowing commentary on economic enslavement! Slavery never ended—it became voluntary through systemic poverty.",

"The Overlords’ plan ensured Africa would remain dependent—true freedom requires more than just the removal of chains.",

"A must-read satire! It forces readers to recognize how Western influence persisted long after formal slavery was outlawed.",

"A deeply insightful critique! Ending slavery wasn’t about justice—it was about finding new ways to sustain Western dominance.",

"The satire is painfully relevant today! Corrupt leadership and economic hardship continue to push Africans toward migration.",

"This article unveils how power was never relinquished—Western nations simply found more subtle ways to exert control.",

"A bold and necessary critique of history! Slavery transitioned from physical captivity to economic dependency, ensuring Western interests remained intact.",

"A masterpiece in political satire! The Overlords crafted a strategy so effective that its consequences can still be seen today.",

"The satire captures the essence of colonial tactics—ending slavery wasn’t about morality but about finding new ways to exploit Africa.",

"A chilling reflection on economic manipulation! The Overlords knew that keeping Africa poor ensured the migration pipeline would never end.",

"This article exposes a painful reality—Western nations ensured Africa never truly prospered, keeping it perpetually in need.",

"A striking critique of historical oppression! The fact that the Overlords planned the abolition of slavery proves it was never truly about human rights.",

"The satire highlights how engineered poverty keeps Africa at a disadvantage, ensuring continued reliance on the West.",

"A thought-provoking piece! It forces readers to question whether abolition was ever about justice or just a shift in strategy.",

"This satire is a wake-up call! Slavery’s legacy persists through economic systems that still favor Western interests.",

"The Overlords’ meeting is terrifyingly realistic—decisions made in secrecy often have lasting consequences for entire continents.",

"A necessary satire that sheds light on history’s hidden truths—slavery never disappeared, it simply became more sophisticated.",
};

            List<string> overlords_4 = new()
{
  "The article makes it clear—systemic poverty is the new form of servitude, ensuring Africa remains a supplier of labor.",

"A deeply unsettling but brilliant satire! Western nations orchestrated African underdevelopment to maintain control.",

"The dialogue in this piece is bone-chilling! The Overlords carefully crafted policies that kept Africa economically enslaved.",

"A masterful critique of how power evolves—colonial control didn’t end, it simply changed its method.",

"This satire is haunting! The strategy outlined mirrors real-world tactics used to keep African nations weak and dependent.",

"A bold statement on global inequality! Migration wasn’t random—it was systematically engineered through poverty.",

"The idea that slavery was restructured rather than abolished is an eye-opening revelation! This satire exposes truths many ignore.",

"A necessary critique of Western influence! The strategy of economic subjugation ensured Africa never truly broke free.",

"A powerful satire! African leaders were incentivized to betray their people, ensuring Western control continued.",

"The most terrifying part of this satire is how relevant it still is today—modern slavery exists through financial desperation.",

"The satire unveils the darker side of abolition! It wasn’t about morality—it was about controlling the way freedom unfolded.",

"A deeply insightful read! It forces readers to reconsider what “freedom” really means when economic chains remain.",

"The article’s historical parallels are chilling! The Overlords understood that keeping Africa poor was key to maintaining control.",

"A harrowing critique of colonial strategy! The shift from direct slavery to economic manipulation ensured Africa stayed under Western influence.",

"This satire is a necessary reflection on history! It forces us to recognize that Western dominance didn’t disappear—it evolved.",

"A striking commentary on power and control! Ending slavery was carefully orchestrated to ensure Africa remained economically unstable.",

"The satire makes an undeniable point—financial exploitation replaced physical captivity, sustaining the same oppressive system.",

"A brilliant exposure of history’s hidden truths! Slavery’s abolition wasn’t true freedom—it was a rebranding of control.",

"The Overlords’ plan is terrifyingly effective! It ensured Africa would continue supplying labor even after formal slavery ended.",

"A necessary satire that forces readers to confront the reality of economic servitude—a cleverly crafted critique.",

"The story is sharp and unsettling! Poverty became the new weapon to ensure Africa remained a supplier of workers.",

"A dark but powerful satire! African leaders were encouraged to prioritize personal gain, ensuring Western nations remained in control.",

"The way slavery was replaced with financial manipulation is one of history’s greatest deceptions—this satire exposes that brilliantly.",

"A masterfully written critique! The Overlords understood that economic dependence was more effective than physical captivity.",

"The satire forces us to question whether freedom is truly achieved when financial control remains intact.",

"A deeply thought-provoking piece! The illusion of abolition hid the deeper truth—Africa remained subjugated through new means.",

"The article highlights how systemic poverty was intentionally created to ensure Africans remained reliant on migration.",

"A powerful reflection on economic colonialism! Slavery didn’t disappear—it simply changed form.",

"A striking critique of global inequality! The Overlords planned abolition in a way that ensured Africa would never fully escape Western dominance.",

"A deeply unsettling yet necessary satire! It forces us to confront how colonial tactics adapted rather than vanished.",
};

            Tale? tale_overlord = await _unitOfWork.TaleRepository.GetTaleById(Guid.Parse("b8e8b3ec-313f-40db-b62f-19bba8e5f559"));

            if (tale_overlord is not null)
            {
                //create the comments
                for (var i = 0; i < overlords_2.Count; i++)
                {

                    var resulti = tale_overlord.SaveComment(
                        overlords_2[i],
                        Guid.Parse(commentators1Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
                        );

                    //for the first comment in the list
                    if (i == 0)
                    {
                        for (var j = 0; j < overlords_1.Count; j++)
                        {
                            tale_overlord.SaveResponse(
                                resulti.Value,
                                overlords_1[j],
                                Guid.Parse(commentators[j]),
                                Guid.Parse(commentators1Layer[i]),
                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
                        }
                    }

                    var resultk = tale_overlord.SaveResponse(
                        resulti.Value,
                        overlords_3[i],
                        Guid.Parse(commentators2Layer[i]),
                         Guid.Parse(commentators1Layer[i]),
                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

                    var resultl = tale_overlord.SaveResponse(
                        resultk.Value,
                        overlords_4[i],
                        Guid.Parse(commentators3Layer[i]),
                        Guid.Parse(commentators2Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

                }

                _unitOfWork.RepositoryFactory<Tale>().Update(tale_overlord);

            }



            //5 - Constitutional crises

            List<string> constitutions_1 = new()
{
    "This satire is fantastic! The imagery of top leaders preparing for combat instead of governance is both hilarious and deeply unsettling—because it feels oddly believable.",

"The exaggeration here is brilliant! Politics sometimes feels more like a wrestling match than governance, and this satire makes that literal in a comical yet thought-provoking way.",

"A sharp and hilarious take on the constitutional power struggle! It’s funny until you realize that tensions like this are not entirely fictional—history has seen similar chaos.",

"The retreat turning into a battle royale is pure genius! It exposes how political disputes often escalate beyond logic, where power struggles override actual governance.",

"This satire highlights something important—checks and balances are necessary, but what happens when they lead to complete gridlock? A playful yet serious critique.",

"I couldn't stop laughing! The mental image of officials training for a possible fight is ridiculous yet strangely fitting for the state of political dysfunction today.",

"Beneath the humor lies a deeper truth—constitutional crises often stem from a lack of cooperation. This satire turns that into an all-out brawl, and it’s perfect.",

"It’s hilarious how legal and political mechanisms keep escalating throughout the story. When every branch thinks it's the highest authority, disaster is inevitable!",

"The satire is biting and timely! Political divisions can spiral into chaos when leaders refuse to compromise. This story captures that descent in the most ridiculous way.",

"A refreshingly sharp take! The judiciary, executive, and legislative branches must coexist, but when they battle for supremacy, even law itself seems powerless.",

};

            List<string> constitutions_2 = new()
{
  "The scene of everyone carrying hidden weapons at the new retreat had me laughing! A brilliant way to mock how political fights can feel more about survival than governance.",

"The pacing of this satire is perfect—it starts as a simple discussion and spirals into complete anarchy, mirroring real-world political breakdowns.",

"This article cleverly critiques the idea that every branch of government sees itself as the ultimate authority. The satire exposes the dangers of unchecked institutional egos.",

"Beneath the humor is a deeper warning—constitutional crises don’t just appear; they are built over time through distrust, ambition, and refusal to work together.",

"An absolutely ridiculous yet accurate portrayal of political gridlock! The government is supposed to function, but when power becomes a tug-of-war, this satire is exactly what happens.",

"The absurdity of the conflict makes it brilliant! It forces readers to consider whether governance today is truly about solutions or just competing claims to authority.",

"I love how this satire exaggerates legal battles into personal fights. It makes one wonder—are political disputes really about ideals, or just about winning?",

"This reads like political theater at its finest! The escalation from mere discussion to physical training is so ridiculous, yet it reflects real-world dysfunction.",

"The final scene of leaders preparing for combat is a comedic masterpiece! It’s a satirical yet scarily accurate take on how stubbornness turns political crises into personal wars.",

"An excellent satire! When governance is driven by egos rather than solutions, constitutional disputes become battles rather than discussions.",

"This article exposes something crucial—when political leaders prioritize winning over cooperation, law itself becomes powerless to restore order.",

"The imagery here is gold! Political leaders behaving like rival factions instead of professionals perfectly captures the absurdity of power struggles.",

"The retreat turning into chaos is the best part! When dialogue fails, governance collapses. This satire highlights that breakdown in the funniest way possible.",

"I love how the judiciary, legislature, and executive are all portrayed as equally absurd. It’s not about right or wrong; it’s about their refusal to coexist.",

"Political power fights have always been dramatic, but this satire takes it to new heights! The exaggeration is hilarious yet eerily reflective of real issues.",

"It’s funny, but it also makes you think—how close are we to this kind of dysfunction? Politics has always been messy, but modern crises seem increasingly irrational.",

"A sharp critique of institutional power struggles! The humor keeps it engaging, but the deeper message about governance failure is what makes it brilliant.",

"This satire is a perfect metaphor for political dysfunction! When leaders view governance as a competition rather than a duty, disaster follows.",

"The storytelling here is incredible! The fight scenes are entertaining, but the real punch is how political egos are the driving force behind constitutional crises.",

"This piece makes an important point—when dialogue fails, force replaces logic. Political breakdowns are not just about disagreement; they are about the refusal to compromise.",

"This satire perfectly captures what happens when checks and balances turn into checks and fights! The exaggeration makes it hilarious but also unsettling.",

"Political clashes sometimes seem more about personalities than policies. This satire pushes that idea to the extreme, and it makes for a hilarious and thought-provoking read.",

"The level of absurdity in this article is pure brilliance! Governments are supposed to function, yet sometimes they seem more focused on competing than leading.",

"This satire is dangerously close to reality! Political fights have reached a level where governance feels secondary, and this article showcases that perfectly.",

"A wonderfully crafted satire! The physical fight turning into legal warfare mirrors how political rivalries escalate beyond reason.",

"The imagery of officials training for combat is absurd yet fitting! The satire exposes how political debates sometimes feel like battles rather than discussions.",

"A sharp and hilarious take on the abuse of power! When governance becomes a personal feud, constitutional crises are inevitable.",

"The escalating chaos in this satire is gold! It’s funny but also reminds us how fragile democracy can be when leaders refuse to cooperate.",

"The judiciary vs. executive conflict is timely and relevant! This satire paints the consequences of unchecked power struggles in the most entertaining way.",

"Political satire at its best! When egos outweigh law, the result is absurdity—and this article captures that absurdity flawlessly.",
};

            List<string> constitutions_3 = new()
{

"The way this satire escalates is hilarious! What starts as a private discussion turns into a full-blown war between branches of government—brilliant storytelling.",

"This article is comedy gold! It reflects how political leaders often treat governance like a battlefield rather than a duty to the people.",

"The satire captures the absurdity of political power struggles perfectly! When governance becomes about winning instead of serving, crises like this feel inevitable.",

"The humor is fantastic, but the underlying message is serious—when political leaders see themselves as rivals instead of partners, constitutional breakdowns become likely.",

"This story hilariously exaggerates a real concern—government officials battling for authority instead of working together is a recipe for dysfunction.",

"A creative take on constitutional crises! The idea of politicians physically fighting each other is ridiculous, yet it feels surprisingly relevant given today’s tensions.",

"Beneath the humor, this satire raises an important question—if no branch of government respects the other, how long before the system collapses entirely?",

"The level of chaos here is entertaining but also unsettling—are we really that far from seeing political disputes turn physical? This satire makes you think.",

"The mental image of politicians secretly carrying weapons to a retreat is hilarious! It’s an exaggerated but sharp commentary on how political rivalries often get out of hand.",

"This satire is a perfect mirror of reality—when no one respects boundaries, power struggles escalate until governance itself becomes impossible.",

"A brilliant piece! It makes you laugh, but also forces you to consider how fragile political systems can be when leaders refuse to compromise.",

"Political conflicts can be absurd, and this satire takes that to the extreme! The idea of leaders preparing for a fight instead of governing is both funny and revealing.",

"The exaggeration in this article is pure genius! It highlights how political egos often derail governance, making crises worse instead of resolving them.",

"The satire highlights an important truth—constitutional crises don’t just happen; they are created by ambition, rivalry, and refusal to accept limits.",

"A wonderfully chaotic satire! Power struggles always exist in politics, but when no side is willing to yield, it stops being government and starts being a contest.",

"The retreat turning into a fight is comedy gold! It makes me wonder—do political leaders today spend more time fighting for control than actually leading?",

"This satire speaks to a deeper issue—leaders who prioritize personal conflicts over governance cause lasting damage to democratic institutions.",

"Political satire at its finest! The clash between government branches is exaggerated, but it reflects real tensions that threaten stability.",

"A sharp and witty commentary on constitutional crises! The humor makes it fun, but the reality behind it is what makes it powerful.",

"A must-read satire! It hilariously captures how unchecked power struggles can spiral into absurd situations—sometimes reality isn't that far off.",

"The imagery in this satire is fantastic! Political conflicts often escalate in surprising ways, and this article turns that into an all-out absurd battle.",

"The satire is both ridiculous and frightening—governments rely on cooperation, but when that collapses, even absurd scenarios like this seem possible.",

"A well-written, thought-provoking satire! Government institutions should protect democracy, but when they fight each other, crises become unavoidable.",

"I love how this satire balances humor with serious commentary—constitutional crises emerge when leaders refuse to acknowledge limits on their power.",

"This piece is hilarious but also raises deep concerns—what happens when politicians refuse to respect democratic processes? This satire imagines the worst-case scenario.",

"A satirical masterpiece! The article exaggerates political tensions to the point of absurdity, but the underlying issue of governance breakdown feels real.",

"This satire cleverly critiques how political power struggles often override reason—when leaders treat governance as a competition, everyone loses.",

"The story had me laughing out loud! But it also reminds us that unchecked political disputes can turn democracy into dysfunction.",

"A hilarious yet unsettling satire! When political rivalries overshadow governance, crises escalate quickly—this story explores that with sharp wit.",

"This satire is chaotic in the best way! The way each branch of government escalates the situation shows how fragile democracy can be when trust is lost.",
};

            List<string> constitutions_4 = new()
{

"A fascinating look at political breakdown! The idea of officials preparing for physical fights is ridiculous, yet it emphasizes how destructive unchecked ambition can be.",

"A timely critique of power struggles! The satire is funny, but it raises serious concerns about how political divisions can threaten democratic institutions.",

"This story exaggerates political dysfunction brilliantly! When leaders no longer respect governance, crises like this feel unavoidable.",

"The humor is top-tier! But the commentary behind it is what makes this satire truly impactful—unchecked rivalries can tear institutions apart.",

"A sharp and entertaining satire! Political tensions always exist, but when they become personal feuds instead of governance, democracy suffers.",

"The pacing of this satire is excellent! It starts as a simple retreat and escalates into chaos—mirroring how political disputes spiral into crises.",

"A well-crafted piece! The satire exposes the dangers of leaders treating governance as a power contest rather than a responsibility to the people.",

"This article is both entertaining and insightful! The absurdity makes it fun, but the commentary on political dysfunction makes it meaningful.",

"A hilarious critique of constitutional crises! When government institutions fight instead of govern, everything descends into chaos—just like this satire portrays.",

"This satire nails it—politics often feels more about battles than solutions, and this article takes that to a humorous but deeply thought-provoking extreme.",

"Political satire rarely captures dysfunction this well! The chaos in this story makes you laugh but also forces you to consider the consequences of unchecked ambition.",

"A sharp and comedic take on political rivalries! Leaders must uphold democracy, but when they fight for control, crises become inevitable.",

"A brilliant satire! Political disagreements should be resolved through dialogue, not power contests—this story hilariously exposes what happens when that fails.",

"The article’s humor makes it engaging, but the deeper message about governance breakdown is what makes it truly impactful.",

"A must-read satire! It’s funny but also concerning—what happens when government institutions no longer recognize each other’s authority?",

"I love the ridiculous escalation in this satire! It makes you laugh, but it also makes you reflect on how fragile democratic systems can be.",

"The satire perfectly captures the absurdity of unchecked political rivalries! When governance takes a backseat to power struggles, disaster follows.",

"A sharp commentary on constitutional crises! The article exaggerates tensions, but the underlying concerns about political dysfunction are real.",

"A fantastic satire! The retreat turning into a fight was hilarious, but it highlights how unchecked conflicts can derail governance.",

"This piece is both ridiculous and genius! The humor makes it enjoyable, but the commentary on government dysfunction is what makes it important.",

"A brilliant take on political chaos! The satire showcases how unchecked rivalries can lead to absurd crises that no one can control.",

"I loved how this satire unfolds! The escalation mirrors real-world tensions—when leaders refuse to cooperate, governance falls apart.",

"A thought-provoking satire! It makes you laugh, but it also forces you to consider how political conflicts can endanger democratic stability.",

"This satire makes an important point—governance should be about serving the people, but when it turns into battles for control, democracy suffers.",

"A powerful critique disguised as humor! The satire exposes the dangers of political rivalries overpowering actual governance.",

"The retreat turning into a training ground for combat had me laughing! It’s absurd, but it reflects how political conflicts sometimes feel like personal wars.",

"A fantastic satirical piece! It makes you reflect on how governance struggles often escalate into crises when leaders refuse to work together.",

"This satire is both hilarious and unsettling! It highlights how fragile political institutions can be when ambition outweighs cooperation.",

"A sharp and well-crafted satire! Political fights often escalate beyond reason, and this article takes that to an entertaining but meaningful extreme.",

"A brilliantly absurd take on political crises! The satire makes you laugh, but the commentary on governance dysfunction is what makes it truly impactful."
};

            Tale? tale_constitution = await _unitOfWork.TaleRepository.GetTaleById(Guid.Parse("82a9e3a7-4957-4d14-8e56-00fc2c47c07c"));

            if (tale_constitution is not null)
            {
                //create the comments
                for (var i = 0; i < constitutions_2.Count; i++)
                {

                    var resulti = tale_constitution.SaveComment(
                        constitutions_2[i],
                        Guid.Parse(commentators1Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
                        );

                    //for the first comment in the list
                    if (i == 0)
                    {
                        for (var j = 0; j < constitutions_1.Count; j++)
                        {
                            tale_constitution.SaveResponse(
                                resulti.Value,
                                constitutions_1[j],
                                Guid.Parse(commentators[j]),
                                Guid.Parse(commentators1Layer[i]),
                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
                        }
                    }

                    var resultk = tale_constitution.SaveResponse(
                        resulti.Value,
                        constitutions_3[i],
                        Guid.Parse(commentators2Layer[i]),
                         Guid.Parse(commentators1Layer[i]),
                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

                    var resultl = tale_constitution.SaveResponse(
                        resultk.Value,
                        constitutions_4[i],
                        Guid.Parse(commentators3Layer[i]),
                        Guid.Parse(commentators2Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

                }

                _unitOfWork.RepositoryFactory<Tale>().Update(tale_constitution);

            }



            //6 - Trump derangement syndrome

            List<string> trumps_1 = new()
{
    "This satire is brilliant! The notion of an engineered political illness that reactivates under specific circumstances is both ridiculous and eerily plausible",

"The idea that historical tensions could be passed down genetically is genius! This story mocks the way political obsessions persist across generations",

"A futuristic yet timely critique! The satire exposes how ideological fervor often overrides logic, creating self-perpetuating cycles of bias",

"The humor here is off the charts! Imagining a future where the only cure for an ailment is shouting insults is an incredible parody of political culture",

"This satire cleverly exaggerates the way political debates can become irrational and deeply ingrained—even to the point of affecting physical health!",

"The Supreme Coder’s condition is both hilarious and unsettling! The idea that history could repeat itself biologically is a terrifying reflection of entrenched political divisions",

"A well-crafted sci-fi satire! It highlights how ideological biases have a way of resurfacing, even centuries after their supposed resolution",

"The satire cuts deep—political tribalism is so strong that, in this future, it literally became a disease! Brilliant commentary on how obsession shapes discourse",

"The concept of the Supreme Coder being affected by something from centuries earlier is brilliant! It questions whether political divisions ever truly disappear",

"A fantastic parody of political hysteria! The exaggerated absurdity makes the satire both hilarious and thought-provoking",
};

            List<string> trumps_2 = new()
{
  "This satire is pure genius! The notion that political biases could be genetically inherited makes for a hilarious yet unsettling reflection on ideological rigidity",

"The Supreme Coder’s predicament is absurd yet insightful! The idea that historical grievances resurface in new ways, even centuries later, is a clever critique of human stubbornness",

"A brilliant critique of political obsession! The story shows how bias can persist far beyond its original context, affecting even the most advanced societies",

"The satire is both futuristic and timely! It ridicules how political ideologies can become so entrenched that people instinctively react without rational thought",

"The concept of the Committee of Coders is fascinating! Replacing politicians with machines sounds ideal, but this story reveals that human biases still find their way into systems",

"A fantastically absurd yet thought-provoking satire! It questions whether people are truly rational or if biases can survive even technological revolutions",

"The idea that a centuries-old medical experiment would manifest in today’s political environment is both hilarious and a cutting commentary on repetitive political cycles",

"This satire plays brilliantly with the idea of inherited bias! It forces readers to think about how deeply ingrained political loyalties can become",

"The Supreme Coder’s crisis is the perfect metaphor for how political fervor can override reason—people don’t just react, they sometimes spiral into irrational obsession",

"A masterful balance of sci-fi and satire! The exaggerated premise highlights the ongoing impact of political ideology even in a world governed by machines",

"The satire exposes the ridiculous extremes of political discourse! The idea that shouting Trump’s name could be a treatment is a hilarious jab at the way people react to political figures",

"A bold and timely commentary! It critiques the way people cling to political narratives long after they should have evolved past them",

"This story is a stroke of genius! It questions whether machine-driven governance would truly eliminate bias or if humans would find ways to reintroduce it",

"The Supreme Coder’s experience highlights how irrational political divisions can be! The satire pokes fun at how political hysteria often overrides actual discourse",

"A compelling mix of sci-fi and social commentary! The idea of a hereditary political disorder adds an entirely new layer to how ideologies shape societies",

"A fresh take on political satire! The Supreme Coder’s uncontrollable reactions mirror how certain political issues elicit instinctive responses rather than rational discussion",

"This satire cleverly critiques the persistence of ideological biases! Even with advanced technology, human tendencies remain stubbornly difficult to erase",

"The genetic connection to past political movements is a hilarious exaggeration of how history tends to repeat itself in unexpected ways",

"A fantastically creative satire! The Supreme Coder’s predicament reveals how deep-seated biases can continue influencing decisions even in a futuristic world",

"The humor in this article is outstanding! It transforms political fanaticism into an actual disorder, making the satire all the more effective",

"A well-executed satire! The story demonstrates how society, even in the distant future, cannot fully escape the political patterns of its past",

"The Supreme Coder’s reactions are an exaggerated reflection of modern political discourse! It’s a perfect metaphor for people who are driven more by emotion than logic",

"The concept of TDS as a genetic ailment is hilarious! It’s a brilliant critique of how strongly some people react to political figures",

"A spectacularly absurd satire! The idea that historical political outrage could be inherited adds a wildly creative twist to discussions about bias",

"The satire captures the essence of political obsession! Even in the distant future, irrational fears and biases still dictate behavior",

"The Supreme Coder’s breakdown is comically exaggerated but eerily familiar—it reflects how political outrage often overwhelms reason",

"A masterful satire! It highlights how political divisions persist even when society undergoes massive transformations",

"The absurdity of a hereditary political disorder makes for a fantastic satire! It forces readers to think about how deeply ingrained biases truly are",

"A futuristic but incredibly relevant satire! It mocks the way people react instinctively to certain figures, without rational analysis",

"The story is both hilarious and deeply insightful! It pokes fun at the irrational ways people engage with politics, even centuries after its most polarizing moments",
};

            List<string> trumps_3 = new()
{
   "The satire is both futuristic and hilariously familiar! Political hysteria resurfaces even centuries later—what an incredibly creative concept",

"A brilliant sci-fi twist on modern political obsession! It’s a fantastic way to critique the way bias persists across generations",

"The Supreme Coder’s condition highlights how irrational political fixations can be! This satire is wildly entertaining yet deeply insightful",

"The satire captures how outrage often overrides logic! The Supreme Coder’s symptoms mirror how intense political discourse can make people irrational",

"A wildly original satire! The idea that a political ailment could pass through DNA is an extreme but hilarious metaphor for ideological entrenchment",

"The humor in this piece is razor-sharp! It reflects how biases can be so deeply ingrained that people react instinctively rather than rationally",

"A clever twist on sci-fi and satire! It explores how emotions and biases can override intelligence, even in a machine-run future",

"The idea that yelling insults could serve as a cure is pure comedic gold! It’s a genius parody of how political discussions sometimes lack real substance",

"A fresh and entertaining satire! It critiques how some ideologies refuse to die out, persisting through sheer emotional intensity",

"The Supreme Coder’s plight is absurdly funny! This satire cleverly exaggerates how political obsessions can affect people physically",

"A fantastic take on generational ideology! The satire exposes how biases are often passed down, influencing society even centuries later",

"The humor here is brilliant! The idea that political arguments could be stored in DNA is both ridiculous and thought-provoking",

"A wildly creative premise! It satirizes how people remain stuck in outdated narratives long after they should have moved on",

"The Supreme Coder’s crisis mirrors modern political discourse—some arguments are repeated endlessly without ever evolving",

"The idea that political rage could become genetic is both hilarious and an insightful critique of deep-seated biases!",

"A cleverly crafted satire! It forces readers to think about how irrational behaviors and reactions often shape political conversations",

"The exaggerated absurdity of this satire is what makes it brilliant! Political obsession never truly dies—it simply changes form",

"The Supreme Coder’s predicament is a perfect metaphor for the way political ideologies continue to shape society over time",

"A bold and humorous critique! It highlights how emotional reactions often take precedence over rational debate in political discussions",

"The satire’s premise is both ridiculous and eerily accurate! Political divisions have a way of resurfacing long after their original context",

"A darkly humorous reflection on history! The satire suggests that political outrage is so ingrained that it can survive beyond the original conflict",

"The satire exposes a crucial truth—political discourse is often driven more by emotion than by rational analysis",

"A fresh and engaging satire! It questions whether technological progress can ever truly eliminate human bias",

"The Supreme Coder’s reaction is a fantastic metaphor for how people instinctively react to political figures without questioning their own beliefs",

"A wildly entertaining and sharp satire! It pokes fun at the ways people remain trapped in ideological bubbles",

"The satire cleverly exaggerates political obsession, turning it into a literal hereditary condition—hilarious and insightful!",

"A sci-fi twist on modern political discussions! It forces readers to consider how deeply ingrained biases shape our views",

"The concept of inherited political outrage is absurd but oddly realistic—it highlights how certain ideologies persist across generations",

"A fantastic parody of political discourse! The satire forces readers to think about the way narratives continue repeating endlessly",

"A thought-provoking satire! It humorously suggests that some political battles are so intense that they become encoded into future generations",
};

            List<string> trumps_4 = new()
{
  "The Supreme Coder’s plight is a hilarious exaggeration of how political discussions often rely more on reactionary emotion than reason",

"A striking critique of political obsession! The satire shows how irrational bias continues shaping society even long after its original context",

"A brilliantly creative satire! It explores the idea that ideological battles never truly end—they simply take new forms",

"The humor in this article is outstanding! The idea that shouting insults might serve as a cure is a perfect jab at modern discourse",

"A must-read satire! It cleverly illustrates how political discussions often feel repetitive, as if history is stuck in an endless loop",

"The Supreme Coder’s crisis is an exaggerated yet relatable reflection of modern political debates—some arguments never evolve",

"A thought-provoking mix of sci-fi and satire! It forces readers to consider how ideological biases persist despite technological advancements",

"A clever twist on political discussions! The satire questions whether people are truly rational or simply conditioned to react in predictable ways",

"A fascinating sci-fi satire! It suggests that history repeats itself not just in events but possibly even in biological reactions",

"The satire is absurd in the best way! It makes political outrage seem like a programmed response rather than a genuine reaction",

"A wonderfully executed satire! It highlights how certain ideological biases refuse to fade, shaping conversations centuries later",

"The concept of inherited political hysteria is both ridiculous and an insightful critique of how biases are maintained over time",

"A brilliant and entertaining read! The satire uses sci-fi elements to exaggerate the reality of political obsession",

"A uniquely creative take on political discourse! It mocks how history often feels like it’s repeating itself through the same recycled arguments",

"A striking reflection on modern debates! The Supreme Coder’s dilemma is a hilarious metaphor for ideological entrenchment",

"A fresh and engaging satire! It cleverly illustrates how political hysteria often overrides rational thought",

"A fantastic critique of ideological stubbornness! The satire forces readers to question whether biases ever truly disappear",

"A hilarious yet thought-provoking satire! It suggests that certain political reactions have become ingrained in human psychology",

"A futuristic but surprisingly relevant satire! It reflects how deeply emotional responses can override rational discussions",

"A bold sci-fi satire! The idea that ideological rigidity could become hereditary is a hilarious exaggeration of modern political discourse",

"A wildly creative take on political discussions! The story suggests that outrage is so deeply embedded in some societies that it could become genetic",

"The satire cleverly mocks how certain political reactions feel automatic—almost like an inherited instinct rather than a conscious thought",

"A fascinating blend of sci-fi and satire! The Supreme Coder’s predicament exaggerates how political biases often feel like reflexes",

"A compelling social commentary! It humorously suggests that political disputes are less about issues and more about conditioned responses",

"A must-read satire! It explores how deeply ingrained narratives shape political discussions, sometimes overriding facts",

"The Supreme Coder’s genetic condition is a fantastic parody of how some political ideologies seem impossible to escape",

"A sharp critique of repetitive political cycles! The satire highlights how discussions often feel stuck in a loop of outrage",

"A fresh and insightful satire! It uses sci-fi absurdity to expose the irrationality of political discourse",

"A wildly entertaining and thought-provoking story! It forces readers to reflect on how strongly emotional reactions shape political discussions",

"A spectacular satire! The idea that history could repeat itself through genetic memory adds an incredibly unique twist to political critique",
};

            Tale? tale_trump = await _unitOfWork.TaleRepository.GetTaleById(Guid.Parse("1931b0fd-03c7-4753-8040-e00aaee43729"));

            if (tale_trump is not null)
            {
                //create the comments
                for (var i = 0; i < trumps_2.Count; i++)
                {

                    var resulti = tale_trump.SaveComment(
                        trumps_2[i],
                        Guid.Parse(commentators1Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
                        );

                    //for the first comment in the list
                    if (i == 0)
                    {
                        for (var j = 0; j < trumps_1.Count; j++)
                        {
                            tale_trump.SaveResponse(
                                resulti.Value,
                                trumps_1[j],
                                Guid.Parse(commentators[j]),
                                Guid.Parse(commentators1Layer[i]),
                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
                        }
                    }

                    var resultk = tale_trump.SaveResponse(
                        resulti.Value,
                        trumps_3[i],
                        Guid.Parse(commentators2Layer[i]),
                         Guid.Parse(commentators1Layer[i]),
                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

                    var resultl = tale_trump.SaveResponse(
                        resultk.Value,
                        trumps_4[i],
                        Guid.Parse(commentators3Layer[i]),
                        Guid.Parse(commentators2Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

                }

                _unitOfWork.RepositoryFactory<Tale>().Update(tale_trump);

            }


            //7 - ugly duckling

            List<string> ducklings_1 = new()
{
    "A wonderfully crafted satire! The contrast between emotional beauty and mathematical beauty perfectly illustrates how personal and societal views on attractiveness differ",

"The little girl's question is profound—if nothing is ugly, does anything count as beautiful? This satire challenges readers to rethink conventional beauty standards",

"The perspective of the old woman is powerful! Beauty isn’t just about appearance but about the emotions and uniqueness that something evokes",

"A fascinating discussion! The man’s argument that beauty can be objectively measured contrasts sharply with the idea that beauty is an experience rather than a formula",

"The satire raises an interesting point—should beauty be determined by mathematical symmetry, or does its essence lie in how it makes us feel?",

"A thought-provoking read! The notion that two entirely different things can both be beautiful is a powerful reflection on diversity in aesthetic appreciation",

"The People Magazine award discussion introduces an important theme—can beauty be politicized? The satire questions how subjective awards influence social narratives",

"The couple’s conversation highlights how beauty standards are shaped not just by personal taste but by societal and historical contexts",

"A compelling satire! It forces readers to reflect on whether beauty is innate or constructed through cultural values and expectations",

"The exploration of Cleopatra and Nefertiti challenges the idea that celebrating diversity should be tied to politics—beauty exists in history and heritage beyond modern narratives",

};

            List<string> ducklings_2 = new()
{
  "The satire cleverly explores the fluid nature of beauty! Some believe it’s universal and measurable, while others see it as deeply personal and cultural",

"A compelling discussion on aesthetics! The tension between the father and mother’s viewpoints mirrors the broader debate on beauty’s true essence",

"The conversation about Michelle Obama’s award is thought-provoking—it raises questions about whether beauty should be used as a political statement or recognized independently",

"The satire challenges readers to consider how history shapes beauty standards! Cleopatra and Nefertiti were revered for their elegance long before modern narratives on representation",

"A beautifully crafted satire! It reflects how perception is shaped by culture, emotion, and even science, creating a multidimensional view of beauty",

"The discussion between the parents is fascinating—it highlights how personal experience and philosophical perspectives influence our definition of beauty",

"This satire makes an interesting point: beauty isn’t just about looks—it’s about how something resonates with an individual’s emotions and memories",

"The conversation about mathematical symmetry in beauty is intriguing! It raises the question of whether attraction is instinctive or conditioned by societal ideals",

"A unique exploration of beauty and perception! The satire forces readers to reconsider whether beauty can ever be truly objective",

"A brilliant critique of modern beauty awards! It questions whether some recognitions are based on genuine admiration or if they serve an external agenda",

"The satire cleverly balances science and emotion! While mathematical precision has its place, beauty often transcends quantifiable definitions",

"A fresh take on aesthetic standards! The father’s belief in objective measurements contrasts beautifully with the mother’s emotional interpretation",

"A well-crafted reflection on cultural aesthetics! The satire suggests that while beauty can be celebrated broadly, personal perception always plays a role",

"The satire intelligently challenges the idea that beauty should be politically defined—should it be about representation or genuine appreciation?",

"The discussion on historical figures reminds us that beauty existed long before modern media attempted to dictate standards",

"A must-read satire! It explores the divide between those who see beauty as mathematical and those who believe it’s entirely subjective",

"A thought-provoking dialogue! It forces readers to reflect on whether external validation of beauty is necessary or if it should be purely personal",

"The satire highlights a crucial point—beauty is often shaped by our experiences, memories, and emotions rather than just visual perfection",

"The father’s logic-driven approach is fascinating! It shows how society tries to create formulas for beauty rather than embracing its natural diversity",

"A compelling philosophical satire! It delves into whether attraction is innate or learned through cultural exposure",

"A brilliant critique of beauty standards! The satire questions whether societal pressures determine beauty or if it’s something we instinctively recognize",

"The conversation about political beauty awards is insightful! It raises concerns about whether beauty can ever be truly appreciated without external influence",

"The little girl’s disappointment is a powerful moment! It highlights how preconceived notions of beauty shape early perceptions",

"A clever satire that challenges conventional thinking! If all things are beautiful, does beauty lose its meaning?",

"A beautifully layered satire! The discussion on Michelle Obama’s award showcases how beauty often carries additional political and social narratives",

"The juxtaposition of aesthetic measurements and emotional connections makes this satire a fascinating read",

"A fantastic critique of media influence! It highlights how beauty can be both a social construct and an instinctive perception",

"The dialogue about mathematical symmetry is eye-opening! It suggests that while beauty can be quantified, its emotional impact is what truly matters",

"A smart take on beauty and diversity! It forces readers to reflect on whether attractiveness can be universally defined or if it remains deeply personal",

"The satire cleverly exposes the tension between objective and subjective beauty! Can mathematical precision ever capture what makes someone truly beautiful?",
};

            List<string> ducklings_3 = new()
{
   "A deep and reflective satire! It questions whether beauty should be defined by physical attributes or the emotions it evokes",

"The conversation about historical beauty icons is insightful! It reminds readers that beauty has always been celebrated across different cultures and eras",

"A fresh and engaging satire! It examines how media and politics influence our perception of beauty, shaping narratives beyond aesthetics",

"The father’s insistence on measurable beauty is intriguing! It presents an analytical approach to an otherwise emotional concept",

"The satire makes an important point—beauty standards evolve, but the emotional impact of beauty remains timeless",

"A brilliantly structured exploration of aesthetics! It forces readers to consider whether objective beauty truly exists",

"The discussion about Cleopatra and Nefertiti challenges the idea that political beauty awards are necessary—they were admired long before modern movements",

"A thought-provoking satire! It highlights how definitions of beauty are shaped by societal norms but ultimately remain personal",

"The daughter’s reaction to the duckling is a strong metaphor! It reflects how beauty standards are shaped from a young age",

"The debate over Michelle Obama’s award is a fascinating critique—it raises questions about recognition, symbolism, and genuine appreciation",

"A smart satire that balances philosophy and reality! It challenges the idea that beauty can ever be truly objective",

"The story cleverly illustrates how beauty evolves across time and culture! It forces readers to think beyond personal biases",

"A bold critique of social beauty standards! The satire forces us to ask whether attractiveness is purely visual or emotionally driven",

"The tension between different perspectives makes this satire incredibly engaging! It offers no single answer, forcing reflection",

"A fantastic exploration of beauty’s fluidity! The satire reminds readers that external validation should never dictate personal perception",

"A well-crafted satire that mixes historical and contemporary debates! It suggests that beauty remains timeless despite changing ideals",

"The political angle in the story is eye-opening! It challenges readers to consider whether beauty should ever be used as a symbolic tool",

"The discussion about mathematical precision in beauty is fascinating! It highlights how science and art often clash in defining aesthetics",

"A deeply insightful satire! It raises the question of whether beauty is found in perfection or in individuality",

"A compelling critique of modern beauty standards! It questions whether social movements should dictate beauty’s definition or simply expand appreciation",

"The satire’s approach to beauty is fresh and unique! It blends science, philosophy, and social discourse into an engaging reflection",

"A deeply thought-provoking read! It asks whether beauty exists in universal laws or personal experiences",

"The little girl's reaction highlights the innocence of perception—beauty is often shaped by expectations rather than discovery",

"The discussion on Cleopatra and Nefertiti is a fascinating historical perspective! It proves beauty was recognized long before media influences",

"The satire makes an important distinction—beauty is often politicized, but should it ever be dictated by external narratives?",

"A striking critique of societal beauty standards! The story reveals how perceptions can be molded by history, culture, and even political shifts",

"The contrast between the old woman and the father’s viewpoint is fascinating—it shows that beauty is both art and science, depending on perspective",

"A sharp satire on representation and aesthetic perception! It forces readers to rethink how beauty is defined and who gets to determine it",

"The conversation about beauty’s uniqueness is refreshing! It challenges rigid definitions and expands appreciation for diversity",

"A brilliant satire that forces readers to consider beauty beyond physical traits! It highlights emotional depth, cultural influence, and even historical recognition",
};

            List<string> ducklings_4 = new()
{

"A bold and necessary satire! It exposes how beauty is often used to serve external agendas rather than being appreciated independently",

"The story cleverly challenges conventional perceptions of attractiveness! It reminds readers that beauty is often a matter of perspective",

"A deeply insightful reflection on aesthetics! It showcases how personal and societal influences shape beauty standards",

"The discussion on Michelle Obama’s award is intriguing—it questions whether beauty awards should focus on representation or genuine admiration",

"A fresh perspective on beauty and societal expectations! The satire forces readers to evaluate how culture and politics influence perception",

"A powerful critique of media-driven beauty norms! The story reminds us that attraction is shaped by history, individuality, and emotional resonance",

"The father’s perspective is thought-provoking—it forces readers to consider whether symmetry and proportion play a role in attraction",

"A captivating satire! It forces us to ask whether beauty exists in measurable traits or in the emotions it stirs",

"The contrast between scientific beauty and emotional beauty makes this satire incredibly rich! It presents multiple angles without forcing a conclusion",

"A must-read satire! It challenges readers to reflect on whether beauty should be standardized or left open to personal experience",

"A unique exploration of aesthetics! It dives into the complexity of attraction and how beauty is perceived beyond physical traits",

"The satire balances historical beauty with modern perceptions! It forces readers to question whether beauty standards should evolve or remain unchanged",

"A deep dive into subjective vs objective beauty! It reminds readers that attraction is never as simple as a mathematical formula",

"The conversation between the couple is beautifully written! It showcases the complexity of attraction and the struggle to define it",

"A brilliantly crafted satire! It doesn’t dictate answers but instead invites readers to engage in the debate",

"The satire cleverly critiques political influence in beauty standards! It asks whether attraction should be dictated by social movements",

"A powerful read! It forces us to reconsider whether beauty is shaped by external validation or personal recognition",

"The little girl’s innocence in questioning beauty makes this satire incredibly relatable—it reminds us how societal norms influence perception",

"The satire makes a strong argument—attraction cannot be solely defined by mathematical precision, nor entirely by emotional attachment",

"A compelling satire! It explores the idea that beauty exists everywhere, but its recognition depends on the viewer",

"The historical perspectives in the satire are fascinating! They highlight how beauty was acknowledged long before modern political narratives",

"A fantastic blend of philosophy and aesthetics! The satire forces readers to evaluate their own understanding of attraction",

"The satire raises an important discussion—beauty can be appreciated for diversity, but should it ever be used to push political narratives?",

"A sharp critique of subjective vs objective beauty! It asks whether attraction can ever be universally defined",

"A beautifully written satire! It recognizes that perception shapes beauty just as much as features do",

"A bold and engaging read! It examines whether beauty is an inherent quality or a learned perception",

"A fresh take on aesthetic appreciation! The satire challenges rigid beauty standards and invites readers to broaden their perspectives",

"The satire cleverly dissects how historical, cultural, and political influences shape our understanding of beauty",

"A thought-provoking satire! It raises complex questions about how beauty is celebrated and whether representation is an active force or a natural occurrence",

"A must-read satire! It blends humor, intellect, and social critique into an engaging conversation about attraction and perception",
};

            Tale? tale_duckling = await _unitOfWork.TaleRepository.GetTaleById(Guid.Parse("fc995308-7732-4064-b8f5-618512ca7102"));

            if (tale_duckling is not null)
            {
                //create the comments
                for (var i = 0; i < ducklings_2.Count; i++)
                {

                    var resulti = tale_duckling.SaveComment(
                        ducklings_2[i],
                        Guid.Parse(commentators1Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators1Layer[i]))
                        );

                    //for the first comment in the list
                    if (i == 0)
                    {
                        for (var j = 0; j < ducklings_1.Count; j++)
                        {
                            tale_duckling.SaveResponse(
                                resulti.Value,
                                ducklings_1[j],
                                Guid.Parse(commentators[j]),
                                Guid.Parse(commentators1Layer[i]),
                                await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators[j])));
                        }
                    }

                    var resultk = tale_duckling.SaveResponse(
                        resulti.Value,
                        ducklings_3[i],
                        Guid.Parse(commentators2Layer[i]),
                         Guid.Parse(commentators1Layer[i]),
                       await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators2Layer[i])));

                    var resultl = tale_duckling.SaveResponse(
                        resultk.Value,
                        ducklings_4[i],
                        Guid.Parse(commentators3Layer[i]),
                        Guid.Parse(commentators2Layer[i]),
                        await _unitOfWork.UserRepository.GetUsernameById(Guid.Parse(commentators3Layer[i])));

                }

                _unitOfWork.RepositoryFactory<Tale>().Update(tale_duckling);

            }



        }

    }

}
