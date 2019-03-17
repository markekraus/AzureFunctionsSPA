import DS from 'ember-data';

export default DS.Model.extend({
  displayName: DS.attr('string'),
  givenName: DS.attr('string'),
  jobTitle: DS.attr('string'),
  mail: DS.attr('string'),
  mobilePhone: DS.attr('string'),
  officeLocation: DS.attr('string'),
  preferredLanguage: DS.attr('string'),
  surname: DS.attr('string'),
  userPrincipalName: DS.attr('string'),
  businessPhones: DS.attr()
});
