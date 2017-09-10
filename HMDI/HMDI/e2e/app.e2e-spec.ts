import { HmdiAppPage } from './app.po';

describe('hmdi-app App', () => {
  let page: HmdiAppPage;

  beforeEach(() => {
    page = new HmdiAppPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
