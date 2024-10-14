(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBDefinitionInformation = factory());
}(this, (function () {
    var index = {
        name: 'fb-definition-information',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'readonly'],
        data: function () {
            return {
                
            };
        },
        computed: {

        },
        watch: {

        },
        created: function () {

        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            subSelectResponseItem(event) {
                this.$emit('select-item', this.value, event, this.iindex, this.cindex, this.sindex, this.iiindex);
            }
        },
        template: `<div class="information" v-if="value.childCode == 'INFORMATION'" v-on:dblclick="subSelectResponseItem($event)">
                        <div class="row instructions" v-if="value.properties != null">
                            <div class="col-md-11 pad-btm">
                                <div v-html="value.properties.instructionText"></div>
                                <img :src="value.properties.instructionImageBase64" v-if="value.properties.instructionImageBase64 != ''" class="img-responsive" />
                            </div>
                            <div class="col-md-1 text-center" v-if="readonly == false">
                                <i class="fa fa-pen edititem" :id="'edititem_' + value.formDefinitionItemID" v-on:click="subSelectResponseItem($event)"></i>
                            </div>
                        </div>
                    </div>`
    };
    return index;
})));