function createForm(emailAddress, jsonConfig) {
  
  var configuration = JSON.parse(jsonConfig);
  
  var form = FormApp.create(configuration.title);

  form.setDescription(configuration.discription);
  form.setLimitOneResponsePerUser(true);
  //form.setAllowResponseEdits(true);
    
  var i = 0;
  configuration.days.forEach(function(day){   
    
    var sectionHeader = form.addSectionHeaderItem();
    sectionHeader.setTitle(day.title);
    
    day.catigories.forEach(function(category){
              
      createDish(form, category.dishes[0], category.title);
            
      for (var i = 1; i < category.dishes.length; ++i) {
        
        createDish(form, category.dishes[i]);
      };
      
    });
       
    i++;
    if (i < configuration.days.length)
    {
      form.addPageBreakItem();
    }
  });  
  
  if (configuration.wishes){  
    createSubmitStep(form, configuration.wishes);
  }
  
  //MailApp.sendEmail('dmitry.aliskerov@gmail.com', configuration.title, form.getPublishedUrl());
  MailApp.sendEmail(emailAddress, configuration.title, form.getPublishedUrl());
  
  
  return 'Url: ' + form.getPublishedUrl() + 'Form Id: ' + form.getId() + ' Configuration: ' + configuration;
}

function responseForm(formId) {
  
 var form = FormApp.openById(formId);
 var formResponses = form.getResponses();
 var result = [];
  
 for (var i = 0; i < formResponses.length; i++) {
   var formResponse = formResponses[i];
   var itemResponses = formResponse.getItemResponses();
   for (var j = 0; j < itemResponses.length; j++) {
     var itemResponse = itemResponses[j];
     var itemResponseId = itemResponse.getItem().getId();
     var itemResponseValue = itemResponse.getResponse();
     
     result.push({ id: itemResponseId, value: itemResponseValue });
   }
 }
  
 return JSON.stringify(result);
}

/* Private */

function createDish(form, dish, categoryTitle) {
  
  var dishItemChoices = [];
  
  var dishItem = form.addCheckboxItem()
  var dishItemChoice = dishItem.createChoice(dish.title);
  
  dishItemChoices.push(dishItemChoice);
  dishItem.setChoices(dishItemChoices);
  
  if (categoryTitle)
  {
    dishItem.setTitle(categoryTitle);
  }
  
  if (dish.image)
  {
    var imgBlob = UrlFetchApp.fetch(dish.image);
    var img = form.addImageItem();
    img.setImage(imgBlob);
    img.setTitle(dish.discription);
  }
}

function createSubmitStep(form, wishes)
{
  form.addPageBreakItem();
  
  var sectionHeader = form.addSectionHeaderItem();
  sectionHeader.setTitle('?????????');

  var feedbackChoices = [];
  
  var feedbackItem = form.addCheckboxItem();
  
  wishes.forEach(function(wish){
    feedbackChoices.push(feedbackItem.createChoice(wish));
  });
    
  feedbackItem.setChoices(feedbackChoices);
  
  feedbackItem.showOtherOption(true);
}