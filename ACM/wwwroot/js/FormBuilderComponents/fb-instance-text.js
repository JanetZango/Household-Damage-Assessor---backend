﻿(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
        typeof define === 'function' && define.amd ? define(factory) :
            (global.VueFBInstanceText = factory());
}(this, (function () {
    var index = {
        name: 'fb-instance-text',
        props: ['value', 'iindex', 'cindex', 'sindex', 'iiindex', 'categoryitems', 'readonly', 'diindex'],
        data: function () {
            return {
                
            };
        },
        watch: {
            textValue: function () {
                this.validate();
            }
        },
        computed: {
            textValue: function () {
                return this.value.instanceProperties.textValue;
            },
            isMandatory: function () {
                var returnValue = false;
                var self = this;

                if (self.value.definitionProperties.isMandatory == true) {
                    returnValue = true;
                }
                else if (self.value != undefined && self.value.definitionProperties != undefined && self.value != null && self.value.definitionProperties != null
                    && self.categoryitems != undefined
                    && self.value.definitionProperties.isCondition == true && self.value.definitionProperties.conditionActionEventCode == 'REQUIRED') {

                    var parentQuestion = $.grep(self.categoryitems, function (item) {
                        var returnValue = false;

                        if (item.childCode == 'QUESTION') {
                            if (item.definitionChildID.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionID.toLowerCase()) {
                                return true;
                            }
                        }

                        return returnValue;
                    });

                    if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                        var selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                            return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                        });

                        if (selectedAnswer != null && selectedAnswer.length > 0) {
                            returnValue = true;
                        }
                    }
                }

                return returnValue;
            },
            visible: function () {
                var returnValue = false;
                var self = this;

                if (self.value != undefined && self.value.definitionProperties != undefined && self.value != null && self.value.definitionProperties != null
                    && self.categoryitems != undefined && self.value.definitionProperties.isCondition == true) {

                    var selectedAnswer;
                    var parentQuestion = $.grep(self.categoryitems, function (item) {
                        var returnValue = false;

                        if (item.childCode == 'QUESTION') {
                            if (item.definitionChildID.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionID.toLowerCase()) {
                                return true;
                            }
                        }

                        return returnValue;
                    });

                    if (self.value.definitionProperties.conditionActionEventCode == 'VISIBLE') {

                        if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                            selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                                return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                            });

                            if (selectedAnswer != null && selectedAnswer.length > 0) {
                                returnValue = true;
                            }
                            else {
                                returnValue = false;
                            }
                        }
                    }
                    else if (self.value.definitionProperties.conditionActionEventCode == 'HIDDEN') {

                        if (parentQuestion != undefined && parentQuestion != null && parentQuestion.length > 0) {
                            selectedAnswer = $.grep(parentQuestion[0].instanceProperties.formItemQuestionResponseID, function (answer) {
                                return answer.toLowerCase() == self.value.definitionProperties.conditionFormDefinitionItemQuestionResponseID.toLowerCase();
                            });

                            if (selectedAnswer != null && selectedAnswer.length > 0) {
                                returnValue = false;
                            }
                            else {
                                returnValue = true;
                            }
                        }
                    }
                    else {
                        returnValue = true;
                    }
                }
                else {
                    returnValue = true;
                }

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
                if (this.isMandatory === true && this.visible == true) {
                    if (this.textValue != null && this.textValue != '') {
                        this.value.isValid = true;
                    }
                    else {
                        this.value.isValid = false;
                    }
                }
                else {
                    this.value.isValid = true;
                }
            },
        },
        template: `<div class="number fb-instance-item" v-if="value.childCode == 'TEXT' && visible == true">
                        <div class="row">
                              <div class="col-md-12">
                                   <strong class="item-description">{{value.childDescription}}</strong> <span style="color:red;" v-if="isMandatory == true">*</span>
                              </div>
                        </div>
                        <div class="row">
                              <div class="col-md-12">
                                    <ul class="nav nav-tabs" role="tablist">
                                        <li class="nav-item">
                                            <a class="nav-link active" v-bind:href="'#fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:id="'fbitemresult-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" role="tab" data-toggle="tab" v-bind:aria-controls="'fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" aria-expanded="true">Result</a></li>
                                        <li class="nav-item">
                                            <a class="nav-link" v-bind:href="'#fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" role="tab" v-bind:id="'fbitemfeedback-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" data-toggle="tab" v-bind:aria-controls="'fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex"><i v-if="(value.instanceItem.remarks != null && value.instanceItem.remarks.length > 0) || (value.instanceItem.files != null && value.instanceItem.files.length > 0)" class="anglo-blue fa fa-check"></i> Feedback</a>
                                        </li>
                                    </ul>
                                    <div class=" tab-content px-1 pt-1 border-left border-bottom border-right">
                                        <div role="tabpanel" class="tab-pane active p-1" v-bind:id="'fbitemresult' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:aria-labelledby="'fbitemresult-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                            <div class="row">
                                                <div class="col-md-6">
                                                     <input type="text" :disabled="readonly" class="form-control" v-if="value.definitionProperties.isMultiline == false" v-model="value.instanceProperties.textValue" @input="update('instanceProperties.textValue', $event.target.value)" />
                                                     <textarea class="form-control" v-if="value.definitionProperties.isMultiline == true" rows="4" v-model="value.instanceProperties.textValue" @input="update('instanceProperties.textValue', $event.target.value)"></textarea>
                                                </div>
                                                <div class="col-md-6">
                                                    <fb-instance-caution-instruction v-model="value"></fb-instance-caution-instruction>
                                                </div>
                                            </div>
                                        </div>
                                        <div role="tabpanel" class="tab-pane" v-bind:id="'fbitemfeedback' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex" v-bind:aria-labelledby="'fbitemfeedback-tab' + value.formDefinitionID + '_' + sindex + '_' + cindex + '_' + iindex + '_' + iiindex + '_' + diindex">
                                              <fb-instance-feedback v-model="value.instanceItem" :readonly="readonly" :isfaulureresultchosen="isFaulureResultChosen()"></fb-instance-feedback>
                                        </div>
                                    </div>
                              </div>
                        </div>
                    </div>`
    }
    return index;
})));