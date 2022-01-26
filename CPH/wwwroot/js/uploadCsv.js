const uploadCsv = Vue.createApp({
    data() {

    },
    methods: {

    },
})

const csvSelector = Vue.createApp({
    el: '#upload-form',
    data() {
        return {
            originalCsv: new File(),
            alteredCsv: new File()
        }
    },
    methods: {
        async getSelectedCsv() {

        }
    },
    watch: {

    }

})