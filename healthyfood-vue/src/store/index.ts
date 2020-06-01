import { IPriceEdit } from './../domain/IPrice/IPriceEdit';
import { IPriceCreate } from './../domain/IPrice/IPriceCreate';
import { ICampaignEdit } from './../domain/ICampaign/ICampaignEdit';
import { ICampaignCreate } from './../domain/ICampaign/ICampaignCreate';
import { ICampaign } from './../domain/ICampaign/ICampaign';
import { IDrinkEdit } from './../domain/IDrink/IDrinkEdit';
import { IDrinkCreate } from './../domain/IDrink/IDrinkCreate';
import { IChangePasswordDTO } from './../types/IChangePasswordDTO';
import { IChangeEmailDTO } from './../types/IChangeEmailDTO';
import { IRegisterDTO } from './../types/IRegisterDTO';
import Vue from 'vue'
import Vuex from 'vuex'
// import createPersistedState from 'vuex-persistedstate';
import { ILoginDTO } from '@/types/ILoginDTO';
import { AccountApi } from '@/services/AccountApi';

import { IDrink } from '@/domain/IDrink/IDrink';
import { DrinkApi } from '@/services/DrinkApi';
import { CampaignApi } from '@/services/CampaignApi';
import { IChangePhoneNumberDTO } from '@/types/IChangePhoneNumberDTO';
import { IPrice } from '@/domain/IPrice/IPrice';
import { PriceApi } from '@/services/PriceApi';

Vue.use(Vuex)

export default new Vuex.Store({
    state: {
        jwt: null as string | null,

        drinks: [] as IDrink[],
        drink: null as IDrink | null,

        campaigns: [] as ICampaign[],
        campaign: null as ICampaign | null,

        prices: [] as IPrice[],
        price: null as IPrice | null,

        userRole: null as string | null,
        userEmail: null as string | null,
        phoneNumber: null as string | null,
        password: null as string | null
    },
    mutations: {
        setJwt(state, jwt: string | null) {
            state.jwt = jwt;
        },

        setDrinks(state, drinks: IDrink[]) {
            state.drinks = drinks;
        },
        setDrink(state, drink: IDrink) {
            state.drink = drink;
        },

        setCampaigns(state, campaigns: ICampaign[]) {
            state.campaigns = campaigns;
        },
        setCampaign(state, campaign: ICampaign) {
            state.campaign = campaign;
        },

        setPrices(state, prices: IPrice[]) {
            state.prices = prices;
        },
        setPrice(state, price: IPrice) {
            console.log("Setting price")
            state.price = price;
        },

        setUserRole(state, userRole: string | null) {
            state.userRole = userRole;
        },
        setUserEmail(state, userEmail: string | null) {
            state.userEmail = userEmail;
        },
        setPhoneNumber(state, phoneNumber: string | null) {
            state.phoneNumber = phoneNumber;
        },
        setPassword(state, password: string | null) {
            state.password = password;
        }
    },
    getters: {
        isAuthenticated(context): boolean {
            return context.jwt !== null;
        },
        userRole(context): string | null {
            return context.userRole;
        },
        userEmail(context): string | null {
            return context.userEmail;
        },
        phoneNumber(context): string | null {
            return context.phoneNumber;
        },
        password(context): string | null {
            return context.password;
        },

        drink(context): IDrink {
            return context.drink as IDrink;
        },
        campaign(context): ICampaign {
            return context.campaign as ICampaign;
        },
        price(context): IPrice {
            return context.price as IPrice;
        }
    },
    actions: {
        clearJwt(context): void {
            context.commit('setJwt', null);
        },
        async changeEmail(context, emailDTO: IChangeEmailDTO): Promise<boolean> {
            const jwt = await AccountApi.changeEmail(emailDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async changePhoneNumber(context, phoneNumberDTO: IChangePhoneNumberDTO): Promise<boolean> {
            const jwt = await AccountApi.changePhoneNumber(phoneNumberDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async changePassword(context, passwordDTO: IChangePasswordDTO): Promise<boolean> {
            const jwt = await AccountApi.changePassword(passwordDTO);
            context.commit('setJwt', jwt);
            return jwt !== null;
        },
        async registerUser(context, registerDTO: IRegisterDTO): Promise<boolean> {
            const jwt = await AccountApi.register(registerDTO);
            context.commit('setJwt', jwt);
            context.commit('setUserEmail', registerDTO.email)
            context.commit('setPassword', registerDTO.password)
            context.commit('setPhoneNumber', registerDTO.phoneNumber)
            return jwt !== null;
        },
        async authenticateUser(context, loginDTO: ILoginDTO): Promise<boolean> {
            const jwt = await AccountApi.getJwt(loginDTO);
            context.commit('setJwt', jwt);
            context.commit('setUserEmail', loginDTO.email)
            context.commit('setPassword', loginDTO.password)
            return jwt !== null;
        },

        async getDrinks(context): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const drinks = await DrinkApi.getAllDrinks(context.state.jwt);
                context.commit('setDrinks', drinks);
            }
        },
        async getDrink(context, id: string): Promise<boolean> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const drink = await DrinkApi.getDrink(id, context.state.jwt);
                context.commit('setDrink', drink);
                return true;
            }
            return false;
        },
        async deleteDrink(context, id: string): Promise<void> {
            console.log('deleteDrink', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await DrinkApi.deleteDrink(id, context.state.jwt);
                await context.dispatch('getDrinks');
            }
        },
        async createDrink(context, drink: IDrinkCreate): Promise<void> {
            console.log('createDrink', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                await DrinkApi.createDrink(drink, context.state.jwt);
                await context.dispatch('getDrinks');
            }
        },
        async updateDrink(context, drink: IDrinkEdit): Promise<void> {
            console.log('editDrink', context.getters.isAuthenticated);
            if (context.getters.isAuthenticated && context.state.jwt) {
                context.commit('setDrink', drink)
                await DrinkApi.updateDrink(drink, context.state.jwt);
                await context.dispatch('getDrinks');
            }
        },

        async getPrices(context): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const prices = await PriceApi.getAllPrices(context.state.jwt);
                context.commit('setPrices', prices);
            }
        },
        async getPrice(context, id: string): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const price = await PriceApi.getPrice(id, context.state.jwt);
                context.commit('setPrice', price);
                console.log("Price has been set")
            }
        },
        async deletePrice(context, id: string): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PriceApi.deletePrice(id, context.state.jwt);
                await context.dispatch('getPrices');
            }
        },
        async createPrice(context, price: IPriceCreate): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                await PriceApi.createPrice(price, context.state.jwt);
                await context.dispatch('getPrices');
            }
        },
        async updatePrice(context, price: IPriceEdit): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                context.commit('setPrice', price)
                await PriceApi.updatePrice(price, context.state.jwt);
                await context.dispatch('getPrices');
            }
        },

        async getCampaigns(context): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const campaigns = await CampaignApi.getAllCampaigns(context.state.jwt);
                context.commit('setCampaigns', campaigns);
            }
        },
        async getCampaign(context, id: string): Promise<boolean> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                const campaign = await CampaignApi.getCampaign(id, context.state.jwt);
                context.commit('setCampaign', campaign);
                return true;
            }
            return false;
        },
        async deleteCampaign(context, id: string): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CampaignApi.deleteCampaign(id, context.state.jwt);
                await context.dispatch('getCampaigns');
            }
        },
        async createCampaign(context, campaign: ICampaignCreate): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                await CampaignApi.createCampaign(campaign, context.state.jwt);
                await context.dispatch('getCampaigns');
            }
        },
        async updateCampaign(context, campaign: ICampaignEdit): Promise<void> {
            if (context.getters.isAuthenticated && context.state.jwt) {
                context.commit('setCampaign', campaign)
                await CampaignApi.updateCampaign(campaign, context.state.jwt);
                await context.dispatch('getCampaigns');
            }
        }
    },
    modules: {
    }
    // plugins: [createPersistedState()]
});
