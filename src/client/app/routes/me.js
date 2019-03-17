import Route from '@ember/routing/route';

export default Route.extend({
  model() {
    return this.store.findAll('me');
  },

  actions: {
    error() {
      window.location.replace('/.auth/login/aad?post_login_redirect_url=/me');
    }
  }
});
