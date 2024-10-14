(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBDefinitionDynamic = factory());
}(this, (function () {
    var index = {
        name: 'fb-definition-dynamic',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'fbselecteditem', 'readonly'],
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
            },
            selectResponseItem(item, event, iindex, cindex, sindex, iiindex) {
                this.$emit('select-item', item, event, iindex, cindex, sindex, iiindex);
            },
            onFBDropItem(event) {
                this.$emit('drop-item', this.sindex, this.cindex, event, this.iindex);
            },
            getPayloadResponse: function (collection, itemIndex) {
                return collection[itemIndex];
            },
        },
        template: `<div class="dynamic" v-if="value.childCode == 'DYNAMIC'">
                        <div class="row" v-on:dblclick="subSelectResponseItem($event)">
                            <div class="col-md-11 question-heading">
                                {{value.childDescription}} <span v-if="value.properties.isMandatory == true">*</span>
                            </div>
                            <div class="col-md-1 text-center" v-if="readonly == false">
                                <i class="fa fa-pen edititem" :id="'edititem_' + value.formDefinitionItemID" v-on:click="subSelectResponseItem($event)"></i>
                            </div>
                        </div>
                        <div class="row" v-on:dblclick="subSelectResponseItem($event)">
                            <div class="col-md-6 question-heading" v-if="value.properties.isPredefinedItems == true">
                                <button type="button" class="btn btn-block btn-default" data-toggle="modal" :data-target="'#dynamic_modal_' + value.formDefinitionItemID">
                                    Select Item
                                </button>
                                <div class="modal fade" :id="'dynamic_modal_' + value.formDefinitionItemID" tabindex="-1" role="dialog">
                                    <div class="modal-dialog modal-sm" role="document">
                                        <div class="modal-content" style="border-radius: 5px;">
                                            <div class="modal-body">
                                                <div>
                                                    <div class="btn-group-vertical full-width" role="group">
                                                        <button v-for="answer in value.properties.responseTypeAnswers" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID" class="btn btn-block btn-default">{{answer.description}}</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 question-heading" v-if="value.properties.isRepeatDynamic == true">
                                <button type="button" class="btn btn-block btn-default">
                                    Add {{value.properties.nameOfItemRepeat}}
                                </button>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12">
                                <div class="items-to-repeat">
                                    Items to repeat
                                </div>
                                <Container group-name="response" :get-child-payload="itemIndex => getPayloadResponse(value.items, itemIndex)" drag-class="fb-preview-drag" @drop="onFBDropItem($event)" class="fb-container" :remove-on-drop-out="true">
                                    <Draggable v-for="(item, iiindex) in value.items" :key="item.formDefinitionItemID">
                                        <div :class="['fb-response-item', 'draggable-item', {'selected': (fbselecteditem != null && fbselecteditem.formDefinitionItemID == item.formDefinitionItemID)}]" v-if="item.properties != undefined && item.properties != null">
                                            <component v-bind:is="'fb-definition-' + item.childCode.toLowerCase()" v-model="value.items[iiindex]" v-bind:iindex="iindex" v-bind:cindex="cindex" v-bind:sindex="sindex" v-bind:iiindex="iiindex" @select-item="selectResponseItem" v-bind:readonly="readonly"></component>
                                        </div>
                                        <div v-if="item.properties == undefined || item.properties == null">
                                            <b>No proporties</b> | {{item.childDescription}}
                                        </div>
                                    </Draggable>
                                    <div v-if="value.items.length == 0" class="mar-all">Drag and drop a response from the palette</div>
                                </Container>
                            </div>
                        </div>
                   </div>`
    };
    return index;
})));