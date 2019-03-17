import DS from 'ember-data';

export default DS.RESTAdapter.extend({
  namespace: '/api',
  pathForType: function() {
    return 'me';
  }
});
