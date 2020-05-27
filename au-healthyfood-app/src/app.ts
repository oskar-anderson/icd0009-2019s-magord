import { IOrder } from './domain/IOrder/IOrder';
import { OrderService } from './service/order-service';
import { AppState } from './state/app-state';
import { autoinject, PLATFORM } from 'aurelia-framework';
import { IAlertData } from 'types/IAlertData';
import { AlertType } from 'types/AlertType';
import {RouterConfiguration, Router} from 'aurelia-router';
import { AuthorizeStep} from 'resources/authorizeStep'

@autoinject
export class App {
    router?: Router;
    private _alert: IAlertData | null = null;
    private _orders: IOrder[] = [];

    constructor(private appState: AppState, private orderService: OrderService) {

    }


    configureRouter(config: RouterConfiguration, router: Router): void {
        this.router = router;

        config.title = "HealthyFood";
        config.addAuthorizeStep(AuthorizeStep);

        config.map([
            { route: ['', 'home', 'home/index'], name: 'home', moduleId: PLATFORM.moduleName('views/home/index'), nav: true, title: 'Home', settings: { roles: [] } },


            { route: ['account/login'], name: 'account-login', moduleId: PLATFORM.moduleName('views/account/login'), nav: false, title: 'Login', settings: { roles: [] } },
            { route: ['account/register'], name: 'account-register', moduleId: PLATFORM.moduleName('views/account/register'), nav: false, title: 'Register', settings: { roles: [] } },


            { route: ['account/manage'], name: 'account-manage', moduleId: PLATFORM.moduleName('views/account/manage/index'), nav: false, title: 'Manage account', settings: { roles: [] } },
            { route: ['account/manageEmail'], name: 'account-manageEmail', moduleId: PLATFORM.moduleName('views/account/manage/email'), nav: false, title: 'Manage email', settings: { roles: [] } },
            { route: ['account/managePassword'], name: 'account-managePassword', moduleId: PLATFORM.moduleName('views/account/manage/password'), nav: false, title: 'Manage password', settings: { roles: [] } },
            { route: ['account/managePhoneNumber'], name: 'account-managePhoneNumber', moduleId: PLATFORM.moduleName('views/account/manage/phoneNumber'), nav: false, title: 'Manage phone number', settings: { roles: [] } },


            { route: ['areas', 'areas/index'], name: 'areas-index', moduleId: PLATFORM.moduleName('views/areas/index'), nav: true, title: 'Areas', settings: { roles: ['admin'], appState: this.appState } },
            { route: ['areas/edit/:id?'], name: 'areas-edit', moduleId: PLATFORM.moduleName('views/areas/edit'), nav: false, title: 'Areas Edit', settings: { roles: ['admin'], appState: this.appState } },
            { route: ['areas/create'], name: 'areas-create', moduleId: PLATFORM.moduleName('views/areas/create'), nav: false, title: 'Areas Create', settings: { roles: ['admin'], appState: this.appState } },


            { route: ['campaigns', 'campaigns/index'], name: 'campaigns-index', moduleId: PLATFORM.moduleName('views/campaigns/index'), nav: true, title: 'Campaigns', settings: { roles: [] } },
            { route: ['campaigns/edit/:id?'], name: 'campaigns-edit', moduleId: PLATFORM.moduleName('views/campaigns/edit'), nav: false, title: 'Campaigns Edit', settings: { roles: [] } },
            { route: ['campaigns/create'], name: 'campaigns-create', moduleId: PLATFORM.moduleName('views/campaigns/create'), nav: false, title: 'Campaigns Create', settings: { roles: [] } },


            { route: ['drinks', 'drinks/index'], name: 'drinks-index', moduleId: PLATFORM.moduleName('views/drinks/index'), nav: true, title: 'Drinks', settings: { roles: [] } },
            { route: ['drinks/edit/:id?'], name: 'drinks-edit', moduleId: PLATFORM.moduleName('views/drinks/edit'), nav: false, title: 'Drinks Edit', settings: { roles: [] } },
            { route: ['drinks/create'], name: 'drinks-create', moduleId: PLATFORM.moduleName('views/drinks/create'), nav: false, title: 'Drinks Create', settings: { roles: [] } },


            { route: ['foods', 'foods/index'], name: 'foods-index', moduleId: PLATFORM.moduleName('views/foods/index'), nav: true, title: 'Foods', settings: { roles: [] } },
            { route: ['foods/edit/:id?'], name: 'foods-edit', moduleId: PLATFORM.moduleName('views/foods/edit'), nav: false, title: 'Foods Edit', settings: { roles: [] } },
            { route: ['foods/create'], name: 'foods-create', moduleId: PLATFORM.moduleName('views/foods/create'), nav: false, title: 'Foods Create', settings: { roles: [] } },


            { route: ['foodtypes', 'foodtypes/index'], name: 'foodtypes-index', moduleId: PLATFORM.moduleName('views/foodtypes/index'), nav: true, title: 'Food types', settings: { roles: ['admin'] } },
            { route: ['foodtypes/edit/:id?'], name: 'foodtypes-edit', moduleId: PLATFORM.moduleName('views/foodtypes/edit'), nav: false, title: 'Food Types Edit', settings: { roles: ['admin'] } },
            { route: ['foodtypes/create'], name: 'foodtypes-create', moduleId: PLATFORM.moduleName('views/foodtypes/create'), nav: false, title: 'Food Types Create', settings: { roles: ['admin'] } },


            { route: ['ingredients', 'ingredients/index'], name: 'ingredients-index', moduleId: PLATFORM.moduleName('views/ingredients/index'), nav: true, title: 'Ingredients', settings: { roles: ['admin'] } },
            { route: ['ingredients/edit/:id?'], name: 'ingredients-edit', moduleId: PLATFORM.moduleName('views/ingredients/edit'), nav: false, title: 'Ingredients Edit', settings: { roles: ['admin'] } },
            { route: ['ingredients/create'], name: 'ingredients-create', moduleId: PLATFORM.moduleName('views/ingredients/create'), nav: false, title: 'Ingredients Create', settings: { roles: ['admin'] } },


            { route: ['orders', 'orders/index'], name: 'orders-index', moduleId: PLATFORM.moduleName('views/orders/index'), nav: true, title: 'My Orders', settings: { roles: [] } },
            { route: ['orders/edit/:id?'], name: 'orders-edit', moduleId: PLATFORM.moduleName('views/orders/edit'), nav: false, title: 'Orders Edit', settings: { roles: [] } },
            { route: ['orders/create'], name: 'orders-create', moduleId: PLATFORM.moduleName('views/orders/create'), nav: false, title: 'Orders Create', settings: { roles: [] } },


            { route: ['orderitems', 'orderitems/index'], name: 'orderitems-index', moduleId: PLATFORM.moduleName('views/orderitems/index'), nav: true, title: 'My cart', settings: { roles: [] } },
            { route: ['orderitems/edit/:id?'], name: 'orderitems-edit', moduleId: PLATFORM.moduleName('views/orderitems/edit'), nav: false, title: 'Order items Edit', settings: { roles: [] } },
            { route: ['orderitems/create'], name: 'orderitems-create', moduleId: PLATFORM.moduleName('views/orderitems/create'), nav: false, title: 'Order items Create', settings: { roles: [] } },
            { route: ['orderitems/orderCheckout'], name: 'orderitems-checkout', moduleId: PLATFORM.moduleName('views/orderitems/orderCheckout'), nav: false, title: 'Order items checkout', settings: { roles: [] } },

            { route: ['ordertypes', 'ordertypes/index'], name: 'ordertypes-index', moduleId: PLATFORM.moduleName('views/ordertypes/index'), nav: true, title: 'Order types', settings: { roles: ['admin'] } },
            { route: ['ordertypes/edit/:id?'], name: 'ordertypes-edit', moduleId: PLATFORM.moduleName('views/ordertypes/edit'), nav: false, title: 'Order Types Edit', settings: { roles: ['admin'] } },
            { route: ['ordertypes/create'], name: 'ordertypes-create', moduleId: PLATFORM.moduleName('views/ordertypes/create'), nav: false, title: 'Order Types Create', settings: { roles: ['admin'] } },


            { route: ['paymenttypes', 'paymenttypes/index'], name: 'paymenttypes-index', moduleId: PLATFORM.moduleName('views/paymenttypes/index'), nav: true, title: 'Payment types', settings: { roles: ['admin'] } },
            { route: ['paymenttypes/edit/:id?'], name: 'paymenttypes-edit', moduleId: PLATFORM.moduleName('views/paymenttypes/edit'), nav: false, title: 'Payment Types Edit', settings: { roles: ['admin'] } },
            { route: ['paymenttypes/create'], name: 'paymenttypes-create', moduleId: PLATFORM.moduleName('views/paymenttypes/create'), nav: false, title: 'Payment Types Create', settings: { roles: ['admin'] } },


            { route: ['prices', 'prices/index'], name: 'prices-index', moduleId: PLATFORM.moduleName('views/prices/index'), nav: true, title: 'Prices', settings: { roles: ['admin'] } },
            { route: ['prices/edit/:id?'], name: 'prices-edit', moduleId: PLATFORM.moduleName('views/prices/edit'), nav: false, title: 'Prices Edit', settings: { roles: ['admin'] } },
            { route: ['prices/create'], name: 'prices-create', moduleId: PLATFORM.moduleName('views/prices/create'), nav: false, title: 'Prices Create', settings: { roles: ['admin'] } },


            { route: ['restaurants', 'restaurants/index'], name: 'restaurants-index', moduleId: PLATFORM.moduleName('views/restaurants/index'), nav: true, title: 'Our restaurants', settings: { roles: [] } },
            { route: ['restaurants/edit/:id?'], name: 'restaurants-edit', moduleId: PLATFORM.moduleName('views/restaurants/edit'), nav: false, title: 'Restaurants Edit', settings: { roles: [] } },
            { route: ['restaurants/create'], name: 'restaurants-create', moduleId: PLATFORM.moduleName('views/restaurants/create'), nav: false, title: 'Restaurants Create', settings: { roles: [] } },


            { route: ['towns', 'towns/index'], name: 'towns-index', moduleId: PLATFORM.moduleName('views/towns/index'), nav: true, title: 'Towns', settings: { roles: ['admin'] } },
            { route: ['towns/edit/:id?'], name: 'towns-edit', moduleId: PLATFORM.moduleName('views/towns/edit'), nav: false, title: 'Towns Edit', settings: { roles: ['admin'] } },
            { route: ['towns/create'], name: 'towns-create', moduleId: PLATFORM.moduleName('views/towns/create'), nav: false, title: 'Towns Create', settings: { roles: ['admin'] } }
        ]);

        config.mapUnknownRoutes('views/home/index');
        config.fallbackRoute('views/home/index')

    }

    logoutOnClick() {
        this.appState.jwt = null;
        this.router!.navigateToRoute('home');
    }
}






