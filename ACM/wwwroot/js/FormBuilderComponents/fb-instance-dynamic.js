(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBInstanceDynamic = factory());
}(this, (function () {
    var index = {
        name: 'fb-instance-dynamic',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'categoryitems', 'readonly', 'diindex'],
        data: function () {
            return {
               
            };
        },
        watch: {
            
        },
        computed: {
            isMandatory: function () {
                var returnValue = false;
                var self = this;


                return returnValue;
            },
            visible: function () {
                var returnValue = true;
                var self = this;

                return returnValue;
            }
        },
        created: function () {
            
        },
        mounted: function () {
            var self = this;
            setTimeout(function () { self.validate(); }, 1000);
        },
        methods: {
            update(key, value) {
                this.$emit('input', _.tap(_.cloneDeep(this.value), v => _.set(v, key, value)));
            },
            isFaulureResultChosen: function () {
                var returnValue = false;

                return returnValue;
            },
            validate: function () {
                if (this.isMandatory === true) {
                    this.value.isValid = true;
                }
                else {
                    this.value.isValid = true;
                }
            },
            SelectExisting: function (answer) {

                var newItem = {
                    formDefinitionItemDynamicResponseID: answer.formBuilderFormDefinitionStandardResponseTypeAnswerID,
                    displayOrder: this.value.instanceProperties.dynamicItems.length,
                    itemName: answer.description,
                    items: []
                };

                var itemsToAdd = _.cloneDeep(this.value.dynamicDefinitionRepeatItems);
                newItem.items = this.generateInstanceItems(itemsToAdd);

                this.value.instanceProperties.dynamicItems.push(newItem);
            },
            AddDynamicItem: function () {
                var newItem = {
                    formDefinitionItemDynamicResponseID: null,
                    displayOrder: this.value.instanceProperties.dynamicItems.length,
                    itemName: "",
                    items: []
                };

                var itemsToAdd = _.cloneDeep(this.value.dynamicDefinitionRepeatItems);
                newItem.items = this.generateInstanceItems(itemsToAdd);

                this.value.instanceProperties.dynamicItems.push(newItem);
            },
            generateInstanceItems: function (itemsToAdd) {
                var returnItems = [];

                for (var i = 0; i < itemsToAdd.length; i++) {
                    var instanceItem = {
                        formDefinitionID: itemsToAdd[i].formDefinitionID,
                        formInstanceItemDynamicID: "00000000-0000-0000-0000-000000000000",
                        childCode: itemsToAdd[i].childCode,
                        childDescription: itemsToAdd[i].childDescription,
                        sequenceNumber: itemsToAdd[i].sequenceNumber,
                        definitionProperties: _.cloneDeep(itemsToAdd[i].properties),
                        definitionChildID: itemsToAdd[i].childID,
                        dynamicDefinitionRepeatItems: [],
                        childID: "00000000-0000-0000-0000-000000000000",
                        isValid: false,
                        instanceItem: {},
                        instanceProperties: {}
                    };

                    switch (instanceItem.childCode) {
                        case "QUESTION": {
                            instanceItem.instanceProperties.formItemQuestionResponseID = [];
                        } break;
                        case "TEXT": {
                            instanceItem.instanceProperties.textValue = "";
                        } break;
                        case "NUMBER": {
                            instanceItem.instanceProperties.numberValue = null;
                        } break;
                        case "SLIDER": {
                            instanceItem.isValid = true;
                            instanceItem.instanceProperties.sliderValue = 0;
                        } break;
                        case "SIGNATURE": {
                            instanceItem.instanceProperties.signatureBase64 = "";
                        } break;
                        case "DATETIME": {
                            instanceItem.instanceProperties.dateValue = moment().format('DD/MM/YYYY');
                            instanceItem.instanceProperties.timeValue = moment().format('hh:mm aa');
                        } break;
                        case "LOCATION": {
                            instanceItem.instanceProperties.locationText = "";
                            instanceItem.instanceProperties.formItemLocationResponseID = "00000000-0000-0000-0000-000000000000";
                        } break;
                        case "DRAWING": {
                            instanceItem.instanceProperties.drawingBase64 = "";
                            instanceItem.instanceProperties.serializedPoints = [];
                        } break;
                        case "PICTURE": {
                            instanceItem.instanceProperties.picturesBase64 = [];
                        } break;
                        case "INFORMATION": {
                            instanceItem.isValid = true;
                        } break;
                    };

                    returnItems.push(instanceItem);
                }

                return returnItems;
            },
            RemoveDynamicItem: function (dynItem, Index) {
                var self = this;
                bootbox.confirm("Are you sure you wish to remove this item?", function (result) {
                    if (result) {
                        self.value.instanceProperties.dynamicItems.splice(Index, 1);
                    }
                });
            }
        },
        template: `<div class="number fb-instance-item" v-if="value.childCode == 'DYNAMIC' && visible == true">
                        <div class="row">
                              <div class="col-md-12">
                                   <strong class="item-description">{{value.childDescription}}</strong> <span style="color:red;" v-if="isMandatory == true">*</span>
                              </div>
                        </div>
                        <template v-for="(dynItem, dindex) in value.instanceProperties.dynamicItems">
                            <div class="panel panel-default">
                                <div class="panel-heading">
                                    <div class="panel-control" v-if="readonly == false">
                                        <button type="button" class="btn btn-dark btn-sm" v-on:click="RemoveDynamicItem(dynItem, dindex)">
                                            <i class="fa fa-times"></i>
                                        </button>
                                    </div>
                                    <h4 class="panel-title" v-if="value.definitionProperties.isPredefinedItems == true">
                                        {{dynItem.itemName}}
                                    </h4>
                                    <h4 class="panel-title" v-if="value.definitionProperties.isRepeatDynamic == true">{{value.definitionProperties.nameOfItemRepeat}} {{dynItem.displayOrder + 1}}</h4>
                                </div>
                                <div class="panel-body">
                                    <div v-if="value.definitionProperties.isRepeatDynamic == true">
                                         {{value.definitionProperties.nameOfItemRepeat}} Name: <input type="text" class="form-control" v-model="dynItem.itemName" /><br /><br />
                                    </div>
                                    <template v-for="(item, iiindex) in dynItem.items">
                                        <component v-bind:is="'fb-instance-' + item.childCode.toLowerCase()" v-model="dynItem.items[iiindex]" v-bind:iindex="iindex"
                                                    v-bind:cindex="cindex" v-bind:sindex="sindex" v-bind:iiindex="iiindex" v-bind:categoryitems="dynItem.items"
                                                    v-bind:readonly="readonly" v-bind:diindex="dindex"></component>
                                    </template>
                                </div>
                            </div>
                        </template><br />
                        <div class="row" v-if="readonly == false">
                            <div class="col-xs-6 question-heading" v-if="value.definitionProperties.isPredefinedItems == true">
                                <button type="button" class="btn btn-block btn-dark" data-toggle="modal" :data-target="'#dynamic_modal_' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                    Select Item
                                </button>
                                <div class="modal" :id="'dynamic_modal_' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" tabindex="-1" role="dialog">
                                    <div class="modal-dialog modal-sm" role="document">
                                        <div class="modal-content" style="border-radius: 5px;">
                                            <div class="modal-body">
                                                <div>
                                                    <div class="btn-group-vertical full-width" role="group">
                                                        <button v-for="answer in value.definitionProperties.responseTypeAnswers" :data-answer-id="answer.formBuilderFormDefinitionStandardResponseTypeAnswerID" data-dismiss="modal" v-on:click="SelectExisting(answer)" class="btn btn-block btn-default">{{answer.description}}</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-6 question-heading" v-if="value.definitionProperties.isRepeatDynamic == true">
                                <button type="button" class="btn btn-block btn-dark" v-on:click="AddDynamicItem()">
                                    Add {{value.definitionProperties.nameOfItemRepeat}}
                                </button>
                            </div>
                        </div>
                    </div>`
    }
    return index;
})));